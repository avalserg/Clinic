using PatientTickets.Application.Abstractions.Messaging;
using PatientTickets.Application.DTOs;

namespace PatientTickets.Application.Handlers.Commands.UpdatePatientTicketHasVisit
{
    public class UpdatePatientTicketHasVisitCommand : ICommand<GetPatientTicketDto>
    {
        public Guid Id { get; init; }
    }
}
