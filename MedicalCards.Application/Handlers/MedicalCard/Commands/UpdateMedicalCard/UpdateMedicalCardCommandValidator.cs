using FluentValidation;

namespace MedicalCards.Application.Handlers.MedicalCard.Commands.UpdateMedicalCard;

internal class UpdateMedicalCardCommandValidator : AbstractValidator<UpdateMedicalCardCommand>
{
    public UpdateMedicalCardCommandValidator()
    {
        RuleFor(e => e.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(e => e.LastName).NotEmpty().MaximumLength(50);
        RuleFor(e => e.Patronymic).NotEmpty().MaximumLength(50);
        RuleFor(e => e.Address).NotEmpty().MaximumLength(100);
        RuleFor(e => e.PhoneNumber).NotEmpty();
    }
}