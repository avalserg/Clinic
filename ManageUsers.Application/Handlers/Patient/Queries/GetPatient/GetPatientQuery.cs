using ManageUsers.Application.DTOs.Patient;
using MediatR;

namespace ManageUsers.Application.Handlers.Patient.Queries.GetPatient;

public class GetPatientQuery : IRequest<GetPatientDto>
{
    public int Id { get; init; } = default!;
}