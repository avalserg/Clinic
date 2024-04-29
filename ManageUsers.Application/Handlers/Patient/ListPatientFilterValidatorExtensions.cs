using FluentValidation;

namespace ManageUsers.Application.Handlers.Patient;

internal class ListPatientFilterValidator : AbstractValidator<ListPatientFilter>
{
    public ListPatientFilterValidator()
    {
        RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
    }
}

public static class ListPatientFilterValidatorExtensions
{
    internal static IRuleBuilderOptions<T, ListPatientFilter> IsValidListUserFilter<T>(this IRuleBuilder<T, ListPatientFilter> ruleBuilder)
    {
        return ruleBuilder
            .SetValidator(new ListPatientFilterValidator());
    }
}