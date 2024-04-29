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

namespace ManageUsers.Application.Handlers.Doctor.Commands.CreateDoctor;

internal class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, CreateApplicationUserDto>
{
    private readonly IBaseWriteRepository<Domain.Doctor> _doctors;
    private readonly IBaseWriteRepository<ApplicationUser> _users;

    private readonly IMapper _mapper;
    
    private readonly PatientsListMemoryCache _listCache;
    
    private readonly ILogger<CreateDoctorCommandHandler> _logger;

    private readonly PatientsCountMemoryCache _countCache;

    public CreateDoctorCommandHandler(
        IBaseWriteRepository<Domain.Doctor> doctors,
        IBaseWriteRepository<ApplicationUser> users,
        IMapper mapper,
        PatientsListMemoryCache listCache,
        ILogger<CreateDoctorCommandHandler> logger,
        PatientsCountMemoryCache countCache)
    {
        _users=users;
        _doctors = doctors;
        _mapper = mapper;
        _listCache = listCache;
        _logger = logger;
        _countCache = countCache;
    }
    /// <summary>
    /// Create ApllicationUser and Patient
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="BadOperationException"></exception>
    public async Task<CreateApplicationUserDto> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
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
            ApplicationUserRole = ApplicationUserRoles.Doctor
        };
        user = await _users.AddAsync(user, cancellationToken);
        var doctor = new Domain.Doctor()
        {

            DateBirthday = request.DateBirthday,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Patronymic = request.Patronymic,
            Address = request.Address,
            Phone = request.Phone,
            CabinetNumber = request.CabinetNumber,
            Category = request.Category,
            Experience = request.Experience,
            Photo = request.Photo,
            ApplicationUserId = newUserGuid
        };
        doctor = await _doctors.AddAsync(doctor, cancellationToken);


        _listCache.Clear();
        _countCache.Clear();
        
        _logger.LogInformation($"New user {doctor.Id} created.");
        
        return _mapper.Map<CreateApplicationUserDto>(user);
    }
}