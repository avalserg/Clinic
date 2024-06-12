using FluentValidation;

namespace PatientTickets.Application.Handlers.Commands.UpdatePatientTicketHasVisit
{
    public class UpdatePatientTicketHasVisitCommandValidator : AbstractValidator<UpdatePatientTicketHasVisitCommand>
    {
        public UpdatePatientTicketHasVisitCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty();
        }
    }
}
