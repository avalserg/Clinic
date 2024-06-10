using FluentValidation;

namespace PatientTickets.Application.Handlers.Commands.DeletePatientTicket;

internal class DeletePatientTicketCommandValidator : AbstractValidator<DeletePatientTicketCommand>
{
    public DeletePatientTicketCommandValidator()
    {
        // RuleFor(e => e.Id).NotEmpty().IsGuid();
    }
}