using FluentValidation;
using ManageUsers.Application.ValidatorsExtensions;

namespace ManageUsers.Application.Handlers.Patient.Queries.GetPatients;

internal class GetPatientsQueryValidator : AbstractValidator<GetPatientsQuery>
{
    public GetPatientsQueryValidator()
    {
        RuleFor(e => e).IsValidListUserFilter();
        RuleFor(e => e).IsValidPaginationFilter();
    }
    
}