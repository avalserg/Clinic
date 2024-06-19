using FluentValidation;

namespace MedicalCards.Application.Handlers.Appointment.Commands.UpdateAppointmentHasPrescription
{
    public class UpdateAppointmentHasPrescriptionCommandValidator : AbstractValidator<UpdateAppointmentHasPrescriptionCommand>
    {
        public UpdateAppointmentHasPrescriptionCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty();
        }
    }
}
