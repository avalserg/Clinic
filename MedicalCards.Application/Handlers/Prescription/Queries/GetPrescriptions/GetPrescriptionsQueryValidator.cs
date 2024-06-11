using FluentValidation;
using MedicalCards.Application.ValidatorsExtensions;

namespace MedicalCards.Application.Handlers.Prescription.Queries.GetPrescriptions
{
    internal class GetPrescriptionsQueryValidator : AbstractValidator<GetPrescriptionsQuery>
    {
        public GetPrescriptionsQueryValidator()
        {
            RuleFor(e => e).IsValidListPrescriptionFilter();
            RuleFor(e => e).IsValidPaginationFilter();
        }
    }
}
