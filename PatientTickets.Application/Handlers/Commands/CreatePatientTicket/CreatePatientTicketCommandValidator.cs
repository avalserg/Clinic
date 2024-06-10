using FluentValidation;

namespace PatientTickets.Application.Handlers.Commands.CreatePatientTicket;

internal class CreatePatientTicketCommandValidator : AbstractValidator<CreatePatientTicketCommand>
{
    public CreatePatientTicketCommandValidator()
    {

    }
}