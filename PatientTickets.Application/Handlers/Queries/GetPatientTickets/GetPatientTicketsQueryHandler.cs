using AutoMapper;
using PatientTickets.Application.Abstractions.Persistence.Repository.Read;
using PatientTickets.Application.BaseRealizations;
using PatientTickets.Application.Caches;
using PatientTickets.Application.DTOs;
using PatientTickets.Domain.Entities;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Application.Handlers.Queries.GetPatientTickets
{
    public class GetPatientTicketQueryHandler : BaseCashedQuery<GetPatientTicketsQuery, Result<BaseListDto<GetPatientTicketDto>>>
    {
        private readonly IBaseReadRepository<PatientTicket> _patientTickets;
        private readonly IMapper _mapper;

        public GetPatientTicketQueryHandler(IBaseReadRepository<PatientTicket> patientTickets, IMapper mapper, PatientTicketsListMemoryCache cache) : base(cache)
        {
            _mapper = mapper;

            _patientTickets = patientTickets;
        }

        public override async Task<Result<BaseListDto<GetPatientTicketDto>>> SentQueryAsync(GetPatientTicketsQuery request, CancellationToken cancellationToken)
        {
            var query = _patientTickets.AsQueryable().Where(ListPatientTicketsWhere.Where(request));
            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }



            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }
            query = query.OrderBy(e => e.DateAppointment);

            var entitiesResult = await _patientTickets.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _patientTickets.AsAsyncRead().CountAsync(query, cancellationToken);
            var items = _mapper.Map<GetPatientTicketDto[]>(entitiesResult);
            return new BaseListDto<GetPatientTicketDto>
            {
                Items = items,
                TotalCount = entitiesCount,
            };
        }
    }
}
