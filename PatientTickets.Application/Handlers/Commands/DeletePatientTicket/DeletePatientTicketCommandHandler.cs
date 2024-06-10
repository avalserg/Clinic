using AutoMapper;
using Microsoft.Extensions.Logging;
using PatientTickets.Application.Abstractions.Messaging;
using PatientTickets.Application.Abstractions.Persistence.Repository.Writing;
using PatientTickets.Application.Abstractions.Service;
using PatientTickets.Application.Caches;
using PatientTickets.Domain.Entities;
using PatientTickets.Domain.Enums;
using PatientTickets.Domain.Errors;
using PatientTickets.Domain.Exceptions.Base;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Application.Handlers.Commands.DeletePatientTicket;

internal class DeletePatientTicketCommandHandler : ICommandHandler<DeletePatientTicketCommand>
{
    private readonly IBaseWriteRepository<PatientTicket> _patientTicket;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly PatientTicketsListMemoryCache _listCache;
    private readonly ILogger<DeletePatientTicketCommandHandler> _logger;
    private readonly PatientTicketsCountMemoryCache _countCache;
    private readonly PatientTicketMemoryCache _patientTicketCache;

    public DeletePatientTicketCommandHandler(
        IBaseWriteRepository<PatientTicket> patientTicket,
        IMapper mapper,
        PatientTicketsListMemoryCache listCache,
        ILogger<DeletePatientTicketCommandHandler> logger,
        PatientTicketsCountMemoryCache countCache,
        ICurrentUserService currentUserService,
        PatientTicketMemoryCache patientTicketCache)
    {

        _patientTicket = patientTicket;
        _mapper = mapper;
        _listCache = listCache;
        _logger = logger;
        _countCache = countCache;
        _currentUserService = currentUserService;
        _patientTicketCache = patientTicketCache;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ForbiddenException"></exception>
    public async Task<Result> Handle(DeletePatientTicketCommand request, CancellationToken cancellationToken)
    {

        var patientTicket = await _patientTicket.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (patientTicket is null)
        {
            return Result.Failure(
                DomainErrors.PatientTicket.PatientTicketNotFound(request.Id));
        }


        if (patientTicket.PatientId != _currentUserService.CurrentUserId && !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }
        await _patientTicket.RemoveAsync(patientTicket, cancellationToken);


        _listCache.Clear();
        _countCache.Clear();
        _logger.LogWarning(
            $"PatientTicket {patientTicket.Id} deleted by {_currentUserService.CurrentUserId}");

        return Result.Success();
    }
}