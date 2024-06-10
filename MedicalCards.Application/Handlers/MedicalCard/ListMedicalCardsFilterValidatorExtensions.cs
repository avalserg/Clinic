using FluentValidation;

namespace MedicalCards.Application.Handlers.MedicalCard;

internal class ListMedicalCardsFilterValidator : AbstractValidator<ListMedicalCardsFilter>
{
    public ListMedicalCardsFilterValidator()
    {
        RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
    }
}

public static class ListMedicalCardsFilterValidatorExtensions
{
    internal static IRuleBuilderOptions<T, ListMedicalCardsFilter> IsValidListUserFilter<T>(this IRuleBuilder<T, ListMedicalCardsFilter> ruleBuilder)
    {
        return ruleBuilder
            .SetValidator(new ListMedicalCardsFilterValidator());
    }
}