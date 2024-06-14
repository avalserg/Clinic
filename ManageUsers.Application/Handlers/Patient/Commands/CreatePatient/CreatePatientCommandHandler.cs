using AutoMapper;
using Contracts;
using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Caches.Patients;
using ManageUsers.Application.DTOs.ApplicationUser;
using ManageUsers.Application.Utils;
using ManageUsers.Domain;
using ManageUsers.Domain.Errors;
using ManageUsers.Domain.Shared;
using ManageUsers.Domain.ValueObjects;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ManageUsers.Application.Handlers.Patient.Commands.CreatePatient;

internal class CreatePatientCommandHandler : ICommandHandler<CreatePatientCommand, CreateApplicationUserDto>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IBaseWriteRepository<Domain.Patient> _patients;
    private readonly IBaseReadRepository<ApplicationUserRole> _userRole;
    private readonly IBaseWriteRepository<Domain.ApplicationUser> _users;
    private readonly IMapper _mapper;
    private readonly PatientsListMemoryCache _listCache;
    private readonly ILogger<CreatePatientCommandHandler> _logger;
    private readonly PatientsCountMemoryCache _countCache;
    private readonly PatientMemoryCache _patientMemoryCache;

    public CreatePatientCommandHandler(
        IBaseWriteRepository<Domain.Patient> patients,
        IBaseWriteRepository<Domain.ApplicationUser> users,
        IMapper mapper,
        PatientsListMemoryCache listCache,
        ILogger<CreatePatientCommandHandler> logger,
        PatientsCountMemoryCache countCache,
        PatientMemoryCache patientMemoryCache,
        IBaseReadRepository<ApplicationUserRole> userRole, IPublishEndpoint publishEndpoint)
    {
        _users = users;
        _patients = patients;
        _mapper = mapper;
        _listCache = listCache;
        _logger = logger;
        _countCache = countCache;
        _patientMemoryCache = patientMemoryCache;
        _userRole = userRole;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result<CreateApplicationUserDto>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var fullName = FullName.Create(request.FirstName, request.LastName, request.Patronymic);

        if (fullName.IsFailure)
        {

            return Result.Failure<CreateApplicationUserDto>(fullName.Error);
        }
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);
        if (phoneNumber.IsFailure)
        {
            return Result.Failure<CreateApplicationUserDto>(phoneNumber.Error);
        }
        var isUserExist = await _users.AsAsyncRead().AnyAsync(e => e.Login == request.Login, cancellationToken);
        if (isUserExist)
        {
            return Result.Failure<CreateApplicationUserDto>(
                DomainErrors.ApplicationUserDomainErrors.LoginAlreadyInUse(request.Login));

        }

        var passwordNumberUse = await _patients.AsAsyncRead()
            .AnyAsync(e => e.PassportNumber == request.PassportNumber, cancellationToken);
        if (passwordNumberUse)
        {
            return Result.Failure<CreateApplicationUserDto>(
                DomainErrors.PatientDomainErrors.PassportNumberAlreadyInUse(request.PassportNumber));
        }
        var newUserGuid = Guid.NewGuid();

        var userRole = await _userRole.AsAsyncRead().FirstOrDefaultAsync(r => r.Name == "Patient", cancellationToken);

        if (userRole == null)
        {
            return Result.Failure<CreateApplicationUserDto>(
                DomainErrors.PatientDomainErrors.PatientRoleNotFound);
        }
        var applicationUser = Domain.ApplicationUser.Create(
            newUserGuid,
            request.Login,
            PasswordHashUtil.Hash(request.Password),
            userRole.ApplicationUserRoleId);

        var patient = Domain.Patient.Create(
            newUserGuid,
            fullName.Value,
            request.DateBirthday,
            request.Address,
            phoneNumber.Value,
            request.PassportNumber,
            request.Avatar,
            newUserGuid);

        applicationUser = await _users.AddAsync(applicationUser, cancellationToken);

        patient = await _patients.AddAsync(patient, cancellationToken);


        await _publishEndpoint.Publish(new UserCreatedEvent
        {
            Id = patient.Id,
            FirstName = patient.FullName.FirstName,
            LastName = patient.FullName.LastName,
            Patronymic = patient.FullName.Patronymic,
            DateBirthday = patient.DateBirthday,
            Address = patient.Address,
            PhoneNumber = patient.PhoneNumber.Value,
        }, cancellationToken);

        _listCache.Clear();
        _countCache.Clear();
        _patientMemoryCache.Clear();
        _logger.LogInformation($"New application user {newUserGuid} created.");

        return _mapper.Map<CreateApplicationUserDto>(applicationUser);
    }
}