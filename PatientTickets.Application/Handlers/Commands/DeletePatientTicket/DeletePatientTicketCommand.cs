using PatientTickets.Application.Abstractions.Messaging;

namespace PatientTickets.Application.Handlers.Commands.DeletePatientTicket;

public class DeletePatientTicketCommand : ICommand
{
    public Guid Id { get; init; } = default!;

}