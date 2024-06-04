using FluentValidation;
using ManageUsers.Domain.ValueObjects;

namespace ManageUsers.Application.Handlers.Doctor.Commands.UpdateDoctor;

internal class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
{
    public UpdateDoctorCommandValidator()
    {
        RuleFor(e => e.FirstName).NotEmpty().MaximumLength(FullName.MaxLength);
        RuleFor(e => e.LastName).NotEmpty().MaximumLength(FullName.MaxLength);
        RuleFor(e => e.Patronymic).NotEmpty().MaximumLength(FullName.MaxLength);
        RuleFor(e => e.Address).NotEmpty().MaximumLength(100);
        RuleFor(e => e.PhoneNumber).NotEmpty();
    }
}