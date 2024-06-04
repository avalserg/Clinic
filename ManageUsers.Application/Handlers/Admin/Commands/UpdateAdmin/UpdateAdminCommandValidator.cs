using FluentValidation;
using ManageUsers.Domain.ValueObjects;

namespace ManageUsers.Application.Handlers.Admin.Commands.UpdateAdmin;

internal class UpdateAdminCommandValidator : AbstractValidator<UpdateAdminCommand>
{
    public UpdateAdminCommandValidator()
    {
        RuleFor(e => e.FirstName).NotEmpty().MaximumLength(FullName.MaxLength);
        RuleFor(e => e.LastName).NotEmpty().MaximumLength(FullName.MaxLength);
        RuleFor(e => e.Patronymic).NotEmpty().MaximumLength(FullName.MaxLength);
       
    }
}