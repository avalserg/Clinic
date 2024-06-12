using FluentValidation;

namespace ManageUsers.Application.Handlers.Doctor.Commands.DeleteDoctor;

internal class DeleteDoctorCommandValidator : AbstractValidator<DeleteDoctorCommand>
{
    public DeleteDoctorCommandValidator()
    {
        RuleFor(e => e.Id).NotEmpty();
    }
}