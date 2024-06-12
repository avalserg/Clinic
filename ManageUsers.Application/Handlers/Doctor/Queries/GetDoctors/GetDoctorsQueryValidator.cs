using FluentValidation;
using ManageUsers.Application.ValidatorsExtensions;

namespace ManageUsers.Application.Handlers.Doctor.Queries.GetDoctors;

internal class GetDoctorsQueryValidator : AbstractValidator<GetDoctorsQuery>
{
    public GetDoctorsQueryValidator()
    {
        RuleFor(e => e).IsValidListUserFilter();
        RuleFor(e => e).IsValidPaginationFilter();
    }

}