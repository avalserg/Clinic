using ManageUsers.Application.DTOs.Patient;
using ManageUsers.Domain.Shared;
using MediatR;

namespace ManageUsers.Application.Handlers.Patient.Queries.GetPatient;

public class GetPatientQuery : IRequest<GetPatientDto>
{
    public Guid Id { get; init; } = default!;
}
//public record GetPatientQuery(Guid Id):IRequest<GetPatientDto>;