using FluentValidation;

namespace ManageUsers.Application.Handlers.Admin;

internal class ListAdminFilterValidator : AbstractValidator<ListAdminFilter>
{
    public ListAdminFilterValidator()
    {
        RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
    }
}

public static class ListAdminFilterValidatorExtensions
{
    internal static IRuleBuilderOptions<T, ListAdminFilter> IsValidListUserFilter<T>(this IRuleBuilder<T, ListAdminFilter> ruleBuilder)
    {
        return ruleBuilder
            .SetValidator(new ListAdminFilterValidator());
    }
}