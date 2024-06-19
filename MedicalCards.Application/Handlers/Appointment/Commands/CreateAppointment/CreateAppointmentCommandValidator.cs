using FluentValidation;

namespace MedicalCards.Application.Handlers.Appointment.Commands.CreateAppointment;

internal class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(e => e.PatientId).NotEmpty();
    }
}