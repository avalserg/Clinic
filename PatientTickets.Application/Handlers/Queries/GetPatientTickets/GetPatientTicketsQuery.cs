using PatientTickets.Application.Abstractions.Messaging;
using PatientTickets.Application.DTOs;

namespace PatientTickets.Application.Handlers.Queries.GetPatientTickets
{
    public class GetPatientTicketsQuery : ListPatientTicketFilter, IBasePaginationFilter, IQuery<BaseListDto<GetPatientTicketDto>>
    {
        public int? Limit { get; init; }
        public int? Offset { get; init; }

    }
}
