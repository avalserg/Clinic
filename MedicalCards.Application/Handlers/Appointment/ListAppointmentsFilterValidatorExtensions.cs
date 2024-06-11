using FluentValidation;

namespace MedicalCards.Application.Handlers.Appointment;

internal class ListAppointmentsFilterValidator : AbstractValidator<ListAppointmentsFilter>
{
    public ListAppointmentsFilterValidator()
    {
        RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
    }
}

public static class ListAppointmentsFilterValidatorExtensions
{
    internal static IRuleBuilderOptions<T, ListAppointmentsFilter> IsValidListUserFilter<T>(this IRuleBuilder<T, ListAppointmentsFilter> ruleBuilder)
    {
        return ruleBuilder
            .SetValidator(new ListAppointmentsFilterValidator());
    }
}