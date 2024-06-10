using PatientTickets.Application.Abstractions.Messaging;
using PatientTickets.Application.DTOs;

namespace PatientTickets.Application.Handlers.Queries.GetPatientTicket
{
    public class GetPatientTicketQuery : IQuery<GetPatientTicketDto>
    {
        public Guid Id { get; init; } = default!;
    }
}
