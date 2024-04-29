using FluentValidation;
using ManageUsers.Application.ValidatorsExtensions;

namespace ManageUsers.Application.Handlers.Doctor.Queries.GetDoctors;

internal class GetDoctorssQueryValidator : AbstractValidator<GetDoctorsQuery>
{
    public GetDoctorssQueryValidator()
    {
        RuleFor(e => e).IsValidListUserFilter();
        RuleFor(e => e).IsValidPaginationFilter();
    }
    
}