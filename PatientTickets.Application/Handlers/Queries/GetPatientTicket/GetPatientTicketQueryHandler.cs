using AutoMapper;
using PatientTickets.Application.Abstractions.Persistence.Repository.Read;
using PatientTickets.Application.BaseRealizations;
using PatientTickets.Application.Caches;
using PatientTickets.Application.DTOs;
using PatientTickets.Domain.Entities;
using PatientTickets.Domain.Errors;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Application.Handlers.Queries.GetPatientTicket
{
    public class GetPatientTicketQueryHandler : BaseCashedQuery<GetPatientTicketQuery, Result<GetPatientTicketDto>>
    {
        private readonly IBaseReadRepository<PatientTicket> _patientTickets;
        private readonly IMapper _mapper;
        public GetPatientTicketQueryHandler(IBaseReadRepository<PatientTicket> patientTickets, IMapper mapper, PatientTicketMemoryCache cache) : base(cache)
        {
            _mapper = mapper;
            _patientTickets = patientTickets;
        }

        public override async Task<Result<GetPatientTicketDto>> SentQueryAsync(GetPatientTicketQuery request, CancellationToken cancellationToken)
        {
            var patientTicket = await _patientTickets.AsAsyncRead().SingleOrDefaultAsync(pt => pt.Id == request.Id, cancellationToken);
            if (patientTicket is null)
            {
                return Result.Failure<GetPatientTicketDto>(
                    DomainErrors.PatientTicket.PatientTicketNotFound(request.Id));
            }


            return _mapper.Map<GetPatientTicketDto>(patientTicket);
        }
    }
}
