using FluentValidation;
using MedicalCards.Application.ValidatorsExtensions;

namespace MedicalCards.Application.Handlers.MedicalCard.Queries.GetMedicalCards;

internal class GetMedicalCardsQueryValidator : AbstractValidator<GetMedicalCardsQuery>
{
    public GetMedicalCardsQueryValidator()
    {
        RuleFor(e => e).IsValidListUserFilter();
        RuleFor(e => e).IsValidPaginationFilter();
    }

}