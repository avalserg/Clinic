using FluentValidation;

namespace ManageUsers.Application.Handlers.Doctor;

internal class ListPatientFilterValidator : AbstractValidator<ListDoctorsFilter>
{
    public ListPatientFilterValidator()
    {
        RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
    }
}

public static class ListDoctorsFilterValidatorExtensions
{
    internal static IRuleBuilderOptions<T, ListDoctorsFilter> IsValidListUserFilter<T>(this IRuleBuilder<T, ListDoctorsFilter> ruleBuilder)
    {
        return ruleBuilder
            .SetValidator(new ListPatientFilterValidator());
    }
}