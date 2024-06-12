using FluentValidation;
using ManageUsers.Domain.ValueObjects;

namespace ManageUsers.Application.Handlers.Patient.Commands.CreatePatient;

internal class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(e => e.Login).MinimumLength(3).MaximumLength(50).NotEmpty();

        RuleFor(e => e.Password).MinimumLength(8).MaximumLength(100).NotEmpty();
        RuleFor(e => e.FirstName).NotEmpty().MaximumLength(FullName.MaxLength);
        RuleFor(e => e.LastName).NotEmpty().MaximumLength(FullName.MaxLength);
        RuleFor(e => e.Patronymic).NotEmpty().MaximumLength(FullName.MaxLength);
        RuleFor(e => e.Address).NotEmpty().MaximumLength(100);
        RuleFor(e => e.PhoneNumber).NotEmpty();
    }
}