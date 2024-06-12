using AutoMapper;
using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Abstractions.Service;
using ManageUsers.Application.Caches.Doctors;
using ManageUsers.Application.DTOs.Doctor;
using ManageUsers.Domain;
using ManageUsers.Domain.Enums;
using ManageUsers.Domain.Errors;
using ManageUsers.Domain.Exceptions.Base;
using ManageUsers.Domain.Shared;
using ManageUsers.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace ManageUsers.Application.Handlers.Doctor.Commands.UpdateDoctor;

internal class UpdateDoctorCommandHandler : ICommandHandler<UpdateDoctorCommand, GetDoctorDto>
{
    private readonly IBaseWriteRepository<Domain.Doctor> _doctors;
    private readonly IBaseReadRepository<ApplicationUserRole> _userRole;
    private readonly IBaseWriteRepository<Domain.ApplicationUser> _users;
    private readonly IMapper _mapper;
    private readonly DoctorsListMemoryCache _listCache;
    private readonly ILogger<UpdateDoctorCommandHandler> _logger;
    private readonly DoctorsCountMemoryCache _countCache;
    private readonly DoctorMemoryCache _doctorMemoryCache;
    private readonly ICurrentUserService _currentUserService;

    public UpdateDoctorCommandHandler(
        IBaseWriteRepository<Domain.Doctor> doctors,
        IBaseWriteRepository<Domain.ApplicationUser> users,
        IMapper mapper,
        DoctorsListMemoryCache listCache,
        ILogger<UpdateDoctorCommandHandler> logger,
        DoctorsCountMemoryCache countCache,
        DoctorMemoryCache doctorMemoryCache, IBaseReadRepository<ApplicationUserRole> userRole, ICurrentUserService currentUserService)
    {
        _users = users;
        _doctors = doctors;
        _mapper = mapper;
        _listCache = listCache;
        _logger = logger;
        _countCache = countCache;
        _doctorMemoryCache = doctorMemoryCache;
        _userRole = userRole;
        _currentUserService = currentUserService;
    }

    public async Task<Result<GetDoctorDto>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {

        var doctor = await _doctors.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (doctor is null)
        {
            return Result.Failure<GetDoctorDto>(DomainErrors.DoctorDomainErrors.NotFound(request.Id));
        }

        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }
        var fullName = FullName.Create(request.FirstName, request.LastName, request.Patronymic);

        if (fullName.IsFailure)
        {
            // log error
            return Result.Failure<GetDoctorDto>(fullName.Error);
        }
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);
        if (phoneNumber.IsFailure)
        {
            return Result.Failure<GetDoctorDto>(phoneNumber.Error);
        }


        var userRole = await _userRole.AsAsyncRead().FirstOrDefaultAsync(r => r.Name == "Doctor", cancellationToken);
        // TODO check role if null


        doctor.Update(
            fullName.Value,
           request.DateBirthday,
           request.Address,
           phoneNumber.Value,
           request.Experience,
           request.CabinetNumber,
            request.Category,
            request.Speciality
           );



        doctor = await _doctors.UpdateAsync(doctor, cancellationToken);


        _listCache.Clear();
        _countCache.Clear();
        _doctorMemoryCache.Clear();
        _logger.LogInformation($"New user {doctor.Id} updated.");

        return _mapper.Map<GetDoctorDto>(doctor);
    }
}