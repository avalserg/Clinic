using FluentValidation;

namespace MedicalCards.Application.Handlers.MedicalCard.Commands.DeleteMedicalCard;

internal class DeleteMedicalCardCommandValidator : AbstractValidator<DeleteMedicalCardCommand>
{
    public DeleteMedicalCardCommandValidator()
    {

    }
}