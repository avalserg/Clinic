using FluentValidation;

namespace MedicalCards.Application.Handlers.Prescription
{
    internal class ListPrescriptionsFilterValidator : AbstractValidator<ListPrescriptionsFilter>
    {
        public ListPrescriptionsFilterValidator()
        {
            RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
        }
    }
    public static class ListPrescriptionsFilterValidationExtensions
    {
        internal static IRuleBuilderOptions<T, ListPrescriptionsFilter> IsValidListPrescriptionFilter<T>(this IRuleBuilder<T, ListPrescriptionsFilter> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new ListPrescriptionsFilterValidator());
        }
    }
}
