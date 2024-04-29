using FluentValidation;

namespace ManageUsers.Application.Handlers.Patient.Commands.CreatePatient;

internal class CreateDoctorCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreateDoctorCommandValidator()
    {
        RuleFor(e => e.Login).MinimumLength(3).MaximumLength(50).NotEmpty();

        RuleFor(e => e.Password).MinimumLength(8).MaximumLength(100).NotEmpty();
    }
}