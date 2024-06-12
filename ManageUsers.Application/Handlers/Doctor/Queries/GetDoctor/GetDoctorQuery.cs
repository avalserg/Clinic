using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.DTOs.Doctor;

namespace ManageUsers.Application.Handlers.Doctor.Queries.GetDoctor;

public class GetDoctorQuery : IQuery<GetDoctorDto>
{
    public Guid Id { get; init; } = default!;
}