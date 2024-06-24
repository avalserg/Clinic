﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using PatientTickets.Application.Abstractions.Caches;
using PatientTickets.Application.Abstractions.Messaging;
using PatientTickets.Application.Abstractions.Persistence.Repository.Writing;

using PatientTickets.Domain.Entities;
using PatientTickets.Domain.Errors;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Application.Handlers.Commands.UpdatePatientTicketHasVisit
{
    public class UpdatePatientTicketHasVisitCommandHandler : ICommandHandler<UpdatePatientTicketHasVisitCommand, bool>
    {
        private readonly IBaseWriteRepository<PatientTicket> _patientTickets;
        private readonly IPatientTicketCache _patientTicketCache;
        private readonly IPatientTicketsListCache _listCache;
        private readonly ILogger<UpdatePatientTicketHasVisitCommand> _logger;
        private readonly IMapper _mapper;

        public UpdatePatientTicketHasVisitCommandHandler(
            IBaseWriteRepository<PatientTicket> patientTickets,
            IMapper mapper, IPatientTicketCache patientTicketCache,
            IPatientTicketsListCache listCache,
            ILogger<UpdatePatientTicketHasVisitCommand> logger)
        {
            _patientTickets = patientTickets;
            _mapper = mapper;
            _patientTicketCache = patientTicketCache;
            _listCache = listCache;
            _logger = logger;
        }
        public async Task<Result<bool>> Handle(UpdatePatientTicketHasVisitCommand request, CancellationToken cancellationToken)
        {
            var patientTicket = await _patientTickets.AsAsyncRead().SingleOrDefaultAsync(pt => pt.Id == request.Id, cancellationToken);
            if (patientTicket is null)
            {
                return Result.Failure<bool>(
                    DomainErrors.PatientTicket.PatientTicketNotFound(request.Id));
            }
            patientTicket.UpdatePatientTicketHasVisit(true);
            await _patientTickets.UpdateAsync(patientTicket, cancellationToken);
            _listCache.Clear();
            _patientTicketCache.Clear();
            _logger.LogInformation($"PatientTicket {patientTicket.Id} has visit updated.");
            return true;
        }
    }
}
