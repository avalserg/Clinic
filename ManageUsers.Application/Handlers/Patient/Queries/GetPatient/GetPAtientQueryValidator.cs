using FluentValidation;

namespace ManageUsers.Application.Handlers.Patient.Queries.GetPatient;

public class GetPatientQueryValidator : AbstractValidator<GetPatientQuery>
{
    public GetPatientQueryValidator()
    {
        RuleFor(e => e.Id).NotEmpty();
    }
}