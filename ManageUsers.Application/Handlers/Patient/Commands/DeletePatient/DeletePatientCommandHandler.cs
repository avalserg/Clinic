using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Abstractions.Service;
using ManageUsers.Application.Caches.Patients;
using ManageUsers.Application.Handlers.Patient.Queries.GetPatient;
using ManageUsers.Domain.Enums;
using ManageUsers.Domain.Errors;
using ManageUsers.Domain.Exceptions.Base;
using ManageUsers.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace ManageUsers.Application.Handlers.Patient.Commands.DeletePatient;

internal class DeletePatientCommandHandler : ICommandHandler<DeletePatientCommand>
{
    private readonly IBaseWriteRepository<Domain.ApplicationUser> _users;
    private readonly IBaseWriteRepository<Domain.Patient> _patient;

    private readonly ICurrentUserService _currentUserService;

    private readonly PatientsListMemoryCache _listCache;

    private readonly PatientsCountMemoryCache _countCache;

    private readonly ILogger<DeletePatientCommandHandler> _logger;

    private readonly PatientMemoryCache _userCache;

    public DeletePatientCommandHandler(
        IBaseWriteRepository<Domain.ApplicationUser> users,
        IBaseWriteRepository<Domain.Patient> patient,
        ICurrentUserService currentUserService,
        PatientsListMemoryCache listCache,
        PatientsCountMemoryCache countCache,
        ILogger<DeletePatientCommandHandler> logger,
        PatientMemoryCache userCache)
    {
        _users = users;
        _patient = patient;
        _currentUserService = currentUserService;
        _listCache = listCache;
        _countCache = countCache;
        _logger = logger;
        _userCache = userCache;
    }

    public async Task<Result> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {

        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }


        var patient = await _patient.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (patient is null)
        {
            return Result.Failure(
                DomainErrors.PatientDomainErrors.NotFound(request.Id));
        }
        var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == patient.ApplicationUserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure(
                DomainErrors.ApplicationUserDomainErrors.NotFound(request.Id));
        }
        await _patient.RemoveAsync(patient, cancellationToken);

        await _users.RemoveAsync(user, cancellationToken);
        _listCache.Clear();
        _countCache.Clear();
        _logger.LogWarning(
            $"User {user.ApplicationUserId} deleted by {_currentUserService.CurrentUserId}");
        _userCache.DeleteItem(new GetPatientQuery { Id = user.ApplicationUserId });
        return Result.Success();
    }


}