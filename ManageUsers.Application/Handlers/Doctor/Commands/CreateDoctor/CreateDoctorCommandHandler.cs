using AutoMapper;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Caches.Doctors;
using ManageUsers.Application.DTOs.ApplicationUser;
using ManageUsers.Application.Utils;
using ManageUsers.Domain;
using ManageUsers.Domain.Errors;
using ManageUsers.Domain.Exceptions.Base;
using ManageUsers.Domain.Shared;
using ManageUsers.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManageUsers.Application.Handlers.Doctor.Commands.CreateDoctor;

internal class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Result<CreateApplicationUserDto>>
{
    private readonly IBaseWriteRepository<Domain.Doctor> _doctors;
    private readonly IBaseWriteRepository<Domain.ApplicationUser> _users;
    private readonly IBaseReadRepository<ApplicationUserRole> _userRole;

    private readonly IMapper _mapper;

    private readonly DoctorsListMemoryCache _listCache;

    private readonly ILogger<CreateDoctorCommandHandler> _logger;

    private readonly DoctorsCountMemoryCache _countCache;

    public CreateDoctorCommandHandler(
        IBaseWriteRepository<Domain.Doctor> doctors,
        IBaseWriteRepository<Domain.ApplicationUser> users,
        IMapper mapper,
        DoctorsListMemoryCache listCache,
        ILogger<CreateDoctorCommandHandler> logger,
        DoctorsCountMemoryCache countCache,
        IBaseReadRepository<ApplicationUserRole> userRole)
    {
        _users = users;
        _doctors = doctors;
        _mapper = mapper;
        _listCache = listCache;
        _logger = logger;
        _countCache = countCache;
        _userRole = userRole;
    }
    /// <summary>
    /// Create ApllicationUser and PatientDomainErrors
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="BadOperationException"></exception>
    public async Task<Result<CreateApplicationUserDto>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        var fullName = FullName.Create(request.FirstName, request.LastName, request.Patronymic);

        if (fullName.IsFailure)
        {
            return Result.Failure<CreateApplicationUserDto>(fullName.Error);
        }
        var isUserExist = await _users.AsAsyncRead().AnyAsync(e => e.Login == request.Login, cancellationToken);
        if (isUserExist)
        {
            return Result.Failure<CreateApplicationUserDto>(DomainErrors.ApplicationUserDomainErrors.LoginAlreadyInUse(request.Login));
        }
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);
        if (phoneNumber.IsFailure)
        {
            return Result.Failure<CreateApplicationUserDto>(phoneNumber.Error);
        }
        var newUserGuid = Guid.NewGuid();
        var userRole = await _userRole.AsAsyncRead().FirstOrDefaultAsync(r => r.Name == "Doctor", cancellationToken);
        // TODO check role if null
        var applicationUser = Domain.ApplicationUser.Create(
            newUserGuid,
            request.Login,
            PasswordHashUtil.Hash(request.Password),
            userRole.ApplicationUserRoleId);
        applicationUser = await _users.AddAsync(applicationUser, cancellationToken);
        var doctor = Domain.Doctor.Create(
            newUserGuid,
            fullName.Value,
            request.DateBirthday,
            request.Address,
            phoneNumber.Value,
            request.Photo,
            request.Experience,
            request.CabinetNumber,
            request.Category,
            newUserGuid,
            request.Speciality);

        doctor = await _doctors.AddAsync(doctor, cancellationToken);


        _listCache.Clear();
        _countCache.Clear();

        _logger.LogInformation($"New user {doctor.Id} created.");

        return _mapper.Map<CreateApplicationUserDto>(applicationUser);
    }
}