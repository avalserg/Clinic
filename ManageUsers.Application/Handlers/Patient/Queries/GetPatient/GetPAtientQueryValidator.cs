using FluentValidation;

namespace ManageUsers.Application.Handlers.Patient.Queries.GetPatient;

public class GetPAtientQueryValidator : AbstractValidator<GetPatientQuery>
{
    public GetPAtientQueryValidator()
    {
        //RuleFor(e => e.Id).NotEmpty().IsGuid();
    }
}