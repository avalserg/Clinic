using FluentValidation;

namespace MedicalCards.Application.Handlers.MedicalCard.Commands.CreateMedicalCard;

internal class CreateMedicalCardCommandValidator : AbstractValidator<CreateMedicalCardCommand>
{
    public CreateMedicalCardCommandValidator()
    {
        RuleFor(e => e.PatientId).NotEmpty();
    }
}