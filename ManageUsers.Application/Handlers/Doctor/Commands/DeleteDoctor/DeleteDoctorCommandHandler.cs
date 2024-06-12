using AutoMapper;
using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Abstractions.Service;
using ManageUsers.Application.Caches.Doctors;
using ManageUsers.Application.Handlers.Doctor.Queries.GetDoctor;
using ManageUsers.Domain;
using ManageUsers.Domain.Enums;
using ManageUsers.Domain.Errors;
using ManageUsers.Domain.Exceptions.Base;
using ManageUsers.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace ManageUsers.Application.Handlers.Doctor.Commands.DeleteDoctor;

internal class DeleteDoctorCommandHandler : ICommandHandler<DeleteDoctorCommand>
{
    private readonly IBaseWriteRepository<Domain.Doctor> _doctors;
    private readonly IBaseWriteRepository<Domain.ApplicationUser> _users;
    private readonly IBaseReadRepository<ApplicationUserRole> _userRole;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    private readonly DoctorsListMemoryCache _listCache;

    private readonly ILogger<DeleteDoctorCommandHandler> _logger;

    private readonly DoctorsCountMemoryCache _countCache;
    private readonly DoctorMemoryCache _doctorCache;

    public DeleteDoctorCommandHandler(
        IBaseWriteRepository<Domain.Doctor> doctors,
        IBaseWriteRepository<Domain.ApplicationUser> users,
        IMapper mapper,
        DoctorsListMemoryCache listCache,
        ILogger<DeleteDoctorCommandHandler> logger,
        DoctorsCountMemoryCache countCache,
        IBaseReadRepository<ApplicationUserRole> userRole, ICurrentUserService currentUserService, DoctorMemoryCache doctorCache)
    {
        _users = users;
        _doctors = doctors;
        _mapper = mapper;
        _listCache = listCache;
        _logger = logger;
        _countCache = countCache;
        _userRole = userRole;
        _currentUserService = currentUserService;
        _doctorCache = doctorCache;
    }
    /// <summary>
    /// Create ApllicationUser and PatientDomainErrors
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="BadOperationException"></exception>
    public async Task<Result> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var userId = request.Id;
        //var u = _currentUserService.CurrentUserId;
        //var role = _currentUserService.CurrentUserRoleEnum;
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }


        var doctor = await _doctors.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == userId, cancellationToken);
        if (doctor is null)
        {
            return Result.Failure(
                DomainErrors.DoctorDomainErrors.NotFound(request.Id));
        }
        var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == doctor.ApplicationUserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure(
                DomainErrors.ApplicationUserDomainErrors.NotFound(request.Id));
        }
        await _doctors.RemoveAsync(doctor, cancellationToken);

        await _users.RemoveAsync(user, cancellationToken);
        _listCache.Clear();
        _countCache.Clear();
        _logger.LogWarning(
            $"User {user.ApplicationUserId} deleted by {_currentUserService.CurrentUserId}");
        _doctorCache.DeleteItem(new GetDoctorQuery { Id = user.ApplicationUserId });
        return Result.Success();
    }
}