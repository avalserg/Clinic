using PatientTickets.Application.Abstractions.Persistence.Repository.Read;
using PatientTickets.Application.BaseRealizations;
using PatientTickets.Application.Caches;
using PatientTickets.Domain.Entities;

namespace PatientTickets.Application.Handlers.Queries.GetCountPatientTickets
{
    public class GetCountPatientTicketsQueryHandler : BaseCashedQuery<GetCountPatientTicketsQuery, int>
    {
        private readonly IBaseReadRepository<PatientTicket> _patientTicketRepository;



        public GetCountPatientTicketsQueryHandler(IBaseReadRepository<PatientTicket> userRepository, PatientTicketsCountMemoryCache cache) : base(cache)
        {
            _patientTicketRepository = userRepository;
        }


        public override async Task<int> SentQueryAsync(GetCountPatientTicketsQuery request, CancellationToken cancellationToken)
        {
            var count = await _patientTicketRepository.AsAsyncRead().CountAsync(cancellationToken);
            return count;
        }
    }
}
