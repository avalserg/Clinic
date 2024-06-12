using AutoMapper;
using Microsoft.Extensions.Logging;
using PatientTickets.Application.Abstractions.Messaging;
using PatientTickets.Application.Abstractions.Persistence.Repository.Writing;
using PatientTickets.Application.Caches;
using PatientTickets.Application.DTOs;
using PatientTickets.Domain.Entities;
using PatientTickets.Domain.Errors;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Application.Handlers.Commands.UpdatePatientTicketHasVisit
{
    public class UpdatePatientTicketHasVisitCommandHandler : ICommandHandler<UpdatePatientTicketHasVisitCommand, GetPatientTicketDto>
    {
        private readonly IBaseWriteRepository<PatientTicket> _patientTickets;
        private readonly PatientTicketMemoryCache _patientTicketMemoryCache;
        private readonly PatientTicketsListMemoryCache _listCache;
        private readonly ILogger<UpdatePatientTicketHasVisitCommand> _logger;
        private readonly IMapper _mapper;

        public UpdatePatientTicketHasVisitCommandHandler(
            IBaseWriteRepository<PatientTicket> patientTickets,
            IMapper mapper, PatientTicketMemoryCache patientTicketMemoryCache,
            PatientTicketsListMemoryCache listCache,
            ILogger<UpdatePatientTicketHasVisitCommand> logger)
        {
            _patientTickets = patientTickets;
            _mapper = mapper;
            _patientTicketMemoryCache = patientTicketMemoryCache;
            _listCache = listCache;
            _logger = logger;
        }
        public async Task<Result<GetPatientTicketDto>> Handle(UpdatePatientTicketHasVisitCommand request, CancellationToken cancellationToken)
        {
            var patientTicket = await _patientTickets.AsAsyncRead().SingleOrDefaultAsync(pt => pt.Id == request.Id, cancellationToken);
            if (patientTicket is null)
            {
                return Result.Failure<GetPatientTicketDto>(
                    DomainErrors.PatientTicket.PatientTicketNotFound(request.Id));
            }
            patientTicket.UpdatePatientTicketHasVisit(true);
            var updatedPatientTicket = await _patientTickets.UpdateAsync(patientTicket, cancellationToken);
            _listCache.Clear();
            _patientTicketMemoryCache.Clear();
            _logger.LogInformation($"PatientTicket {patientTicket.Id} has visit updated.");
            return _mapper.Map<GetPatientTicketDto>(updatedPatientTicket);
        }
    }
}
