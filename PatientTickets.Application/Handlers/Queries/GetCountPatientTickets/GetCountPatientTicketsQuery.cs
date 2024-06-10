using MediatR;

namespace PatientTickets.Application.Handlers.Queries.GetCountPatientTickets
{
    public class GetCountPatientTicketsQuery : ListPatientTicketFilter, IRequest<int>
    {
    }
}
