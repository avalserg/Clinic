using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Abstractions.Service;
using ManageUsers.Application.Caches;
using ManageUsers.Application.Exceptions;
using ManageUsers.Application.Handlers.Patient.Queries.GetPatient;
using ManageUsers.Domain;
using ManageUsers.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManageUsers.Application.Handlers.Patient.Commands.DeletePatient;

internal class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand>
{
    private readonly IBaseWriteRepository<ApplicationUser> _users;
    private readonly IBaseWriteRepository<Domain.Patient> _patient;

    private readonly ICurrentUserService _currentUserService;
    
    private readonly PatientsListMemoryCache _listCache;
    
    private readonly PatientsCountMemoryCache _countCache;
    
    private readonly ILogger<DeletePatientCommandHandler> _logger;

    private readonly PatientMemoryCache _userCase;

    public DeletePatientCommandHandler(
        IBaseWriteRepository<ApplicationUser> users, 
        IBaseWriteRepository<Domain.Patient> patient, 
        ICurrentUserService currentUserService,
        PatientsListMemoryCache listCache,
        PatientsCountMemoryCache countCache,
        ILogger<DeletePatientCommandHandler> logger,
        PatientMemoryCache userCase)
    {
        _users = users;
        _patient = patient;
        _currentUserService = currentUserService;
        _listCache = listCache;
        _countCache = countCache;
        _logger = logger;
        _userCase = userCase;
    }
    
    public async Task Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.Id);
        var u = _currentUserService.CurrentUserId;
        if (!_currentUserService.UserInRole(ApplicationUserRoles.Admin))
        {
            throw new ForbiddenException();
        }

        var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == userId, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException(request);
        }
        var patient = await _patient.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == user.ApplicationUserId, cancellationToken);
        if (patient is null)
        {
            throw new NotFoundException(request);
        }
        await _patient.RemoveAsync(patient, cancellationToken);

        await _users.RemoveAsync(user, cancellationToken);
        _listCache.Clear();
        _countCache.Clear();
        _logger.LogWarning($"User {user.ApplicationUserId.ToString()} deleted by {_currentUserService.CurrentUserId.ToString()}");
        //_userCase.DeleteItem(new GetPatientQuery() {Id = user.ApplicationUserId});
    }
}