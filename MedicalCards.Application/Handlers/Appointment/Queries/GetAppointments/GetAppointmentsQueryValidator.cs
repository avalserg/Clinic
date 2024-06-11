using FluentValidation;
using MedicalCards.Application.ValidatorsExtensions;

namespace MedicalCards.Application.Handlers.Appointment.Queries.GetAppointments
{
    public class GetAppointmentsQueryValidator : AbstractValidator<GetAppointmentsQuery>
    {
        public GetAppointmentsQueryValidator()
        {
            RuleFor(e => e).IsValidListUserFilter();
            RuleFor(e => e).IsValidPaginationFilter();
        }
    }
}
