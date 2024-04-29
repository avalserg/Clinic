using ManageUsers.Application.DTOs.Doctor;
using ManageUsers.Application.DTOs.Patient;
using MediatR;

namespace ManageUsers.Application.Handlers.Doctor.Queries.GetDoctor;

public class GetDoctorQuery : IRequest<GetDoctorDto>
{
    public int Id { get; init; } = default!;
}