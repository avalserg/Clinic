using PatientTickets.Application.Abstractions.Messaging;

namespace PatientTickets.Application.Handlers.Commands.UpdatePatientTicketHasVisit
{
    public class UpdatePatientTicketHasVisitCommand : ICommand<bool>
    {
        public Guid Id { get; init; }
    }
}
