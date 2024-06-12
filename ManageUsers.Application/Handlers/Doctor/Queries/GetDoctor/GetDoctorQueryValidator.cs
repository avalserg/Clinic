using FluentValidation;

namespace ManageUsers.Application.Handlers.Doctor.Queries.GetDoctor;

public class GetDoctorQueryValidator : AbstractValidator<GetDoctorQuery>
{
    public GetDoctorQueryValidator()
    {
        RuleFor(e => e.Id).NotEmpty();
    }
}