using AutoMapper;
using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Caches;
using ManageUsers.Application.DTOs;
using ManageUsers.Application.Exceptions;
using ManageUsers.Application.Utils;
using ManageUsers.Domain;
using ManageUsers.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManageUsers.Application.Handlers.Patient.Commands.CreatePatient;

internal class CreateDoctorCommandHandler : IRequestHandler<CreatePatientCommand, CreateApplicationUserDto>
{
    private readonly IBaseWriteRepository<Domain.Patient> _patients;
    private readonly IBaseWriteRepository<ApplicationUser> _users;

    private readonly IMapper _mapper;
    
    private readonly PatientsListMemoryCache _listCache;
    
    
    private readonly ILogger<CreateDoctorCommandHandler> _logger;

    private readonly PatientsCountMemoryCache _countCache;
    private readonly PatientMemoryCache _patientMemoryCache;

    public CreateDoctorCommandHandler(
        IBaseWriteRepository<Domain.Patient> patients,
        IBaseWriteRepository<ApplicationUser> users,
        IMapper mapper,
        PatientsListMemoryCache listCache,
        ILogger<CreateDoctorCommandHandler> logger,
        PatientsCountMemoryCache countCache,
        PatientMemoryCache patientMemoryCache)
    {
        _users=users;
        _patients = patients;
        _mapper = mapper;
        _listCache = listCache;
        _logger = logger;
        _countCache = countCache;
        _patientMemoryCache = patientMemoryCache;
    }
    /// <summary>
    /// Create ApllicationUser and Patient
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="BadOperationException"></exception>
    public async Task<CreateApplicationUserDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = await _users.AsAsyncRead().AnyAsync(e => e.Login == request.Login, cancellationToken);
        if (isUserExist)
        {
            throw new BadOperationException($"User with login {request.Login} already exists.");
        }
        var newUserGuid = Guid.NewGuid();
       
        var user = new ApplicationUser()
        {
            ApplicationUserId = newUserGuid,
            PasswordHash = PasswordHashUtil.Hash(request.Password),
            Login = request.Login,
            ApplicationUserRole = ApplicationUserRoles.Patient
        };
        user = await _users.AddAsync(user, cancellationToken);
        var patient = new Domain.Patient()
        {

            DateBirthday = request.DateBirthday,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Patronymic = request.Patronymic,
            Address = request.Address,
            Phone = request.Phone,
            Avatar = request.Avatar,
            ApplicationUserId = newUserGuid
        };
        patient = await _patients.AddAsync(patient, cancellationToken);


        _listCache.Clear();
        _countCache.Clear();
        _patientMemoryCache.Clear();
        _logger.LogInformation($"New user {patient.Id} created.");
        
        return _mapper.Map<CreateApplicationUserDto>(user);
    }
}