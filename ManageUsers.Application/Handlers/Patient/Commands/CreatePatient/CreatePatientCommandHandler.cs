using AutoMapper;
using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Caches.Patients;
using ManageUsers.Application.DTOs.ApplicationUser;
using ManageUsers.Application.Utils;
using ManageUsers.Domain.Enums;
using ManageUsers.Domain.Errors;
using ManageUsers.Domain.Shared;
using ManageUsers.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace ManageUsers.Application.Handlers.Patient.Commands.CreatePatient;

internal class CreatePatientCommandHandler : ICommandHandler<CreatePatientCommand, CreateApplicationUserDto>
{
    private readonly IBaseWriteRepository<Domain.Patient> _patients;
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
        PatientMemoryCache patientMemoryCache)
    {
        _users = users;
        _patients = patients;
        _mapper = mapper;
        _listCache = listCache;
        _logger = logger;
        _countCache = countCache;
        _patientMemoryCache = patientMemoryCache;
    }

    public async Task<Result<CreateApplicationUserDto>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var fullName = FullName.Create(request.FirstName, request.LastName, request.Patronymic);

        if (fullName.IsFailure)
        {
            // log error
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
            //throw new BadOperationException($"User with login {request.Login} already exists.");
        }
        var newUserGuid = Guid.NewGuid();


        var applicationUser = Domain.ApplicationUser.Create(
            newUserGuid,
            request.Login,
            PasswordHashUtil.Hash(request.Password),
            ApplicationUserRolesEnum.Patient);

        var patient = Domain.Patient.Create(
            newUserGuid,
            fullName.Value,
            request.DateBirthday,
            request.Address,
            phoneNumber.Value,
            request.Avatar,
            newUserGuid);

        applicationUser = await _users.AddAsync(applicationUser, cancellationToken);

        patient = await _patients.AddAsync(patient, cancellationToken);


        _listCache.Clear();
        _countCache.Clear();
        _patientMemoryCache.Clear();
        _logger.LogInformation($"New user {patient.Id} created.");

        return _mapper.Map<CreateApplicationUserDto>(applicationUser);
    }
}