using FluentValidation;
using ManageUsers.Application.ValidatorsExtensions;

namespace ManageUsers.Application.Handlers.Patient.Commands.DeletePatient;

internal class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
{
    public DeletePatientCommandValidator()
    {
       // RuleFor(e => e.Id).NotEmpty().IsGuid();
    }
}