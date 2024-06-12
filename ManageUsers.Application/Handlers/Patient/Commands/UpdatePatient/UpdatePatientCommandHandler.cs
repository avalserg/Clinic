using AutoMapper;
using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Abstractions.Service;
using ManageUsers.Application.Caches.Patients;
using ManageUsers.Application.DTOs.Patient;
using ManageUsers.Application.Handlers.Patient.Commands.CreatePatient;
using ManageUsers.Domain;
using ManageUsers.Domain.Enums;
using ManageUsers.Domain.Errors;
using ManageUsers.Domain.Exceptions.Base;
using ManageUsers.Domain.Shared;
using ManageUsers.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace ManageUsers.Application.Handlers.Patient.Commands.UpdatePatient;

internal class UpdatePatientCommandHandler : ICommandHandler<UpdatePatientCommand, GetPatientDto>
{
    private readonly IBaseWriteRepository<Domain.Patient> _patients;
    private readonly IBaseReadRepository<ApplicationUserRole> _userRole;
    private readonly IBaseWriteRepository<Domain.ApplicationUser> _users;
    private readonly IMapper _mapper;
    private readonly PatientsListMemoryCache _listCache;
    private readonly ILogger<CreatePatientCommandHandler> _logger;
    private readonly PatientsCountMemoryCache _countCache;
    private readonly PatientMemoryCache _patientMemoryCache;
    private readonly ICurrentUserService _currentUserService;

    public UpdatePatientCommandHandler(
        IBaseWriteRepository<Domain.Patient> patients,
        IBaseWriteRepository<Domain.ApplicationUser> users,
        IMapper mapper,
        PatientsListMemoryCache listCache,
        ILogger<CreatePatientCommandHandler> logger,
        PatientsCountMemoryCache countCache,
        PatientMemoryCache patientMemoryCache, IBaseReadRepository<ApplicationUserRole> userRole, ICurrentUserService currentUserService)
    {
        _users = users;
        _patients = patients;
        _mapper = mapper;
        _listCache = listCache;
        _logger = logger;
        _countCache = countCache;
        _patientMemoryCache = patientMemoryCache;
        _userRole = userRole;
        _currentUserService = currentUserService;
    }

    public async Task<Result<GetPatientDto>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {

        var patient = await _patients.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (patient is null)
        {
            return Result.Failure<GetPatientDto>(DomainErrors.PatientDomainErrors.NotFound(request.Id));
        }
        var passwordNumberUse = await _patients.AsAsyncRead()
            .AnyAsync(e => e.PassportNumber == request.PassportNumber, cancellationToken);
        if (passwordNumberUse && patient.PassportNumber != request.PassportNumber)
        {
            return Result.Failure<GetPatientDto>(
                DomainErrors.PatientDomainErrors.PassportNumberAlreadyInUse(request.PassportNumber));
        }
        if (request.Id != _currentUserService.CurrentUserId &&
            !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }
        var fullName = FullName.Create(request.FirstName, request.LastName, request.Patronymic);

        if (fullName.IsFailure)
        {
            // log error
            return Result.Failure<GetPatientDto>(fullName.Error);
        }
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);
        if (phoneNumber.IsFailure)
        {
            return Result.Failure<GetPatientDto>(phoneNumber.Error);
        }

        patient.Update(
            fullName.Value,
           request.DateBirthday,
           request.Address,
           phoneNumber.Value,
           request.PassportNumber,
           request.Avatar
           );

        patient = await _patients.UpdateAsync(patient, cancellationToken);

        _listCache.Clear();
        _countCache.Clear();
        _patientMemoryCache.Clear();
        _logger.LogInformation($"New user {patient.Id} updated.");

        return _mapper.Map<GetPatientDto>(patient);
    }
}