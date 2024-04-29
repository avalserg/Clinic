using ManageUsers.Application.DTOs;
using ManageUsers.Application.DTOs.Patient;
using MediatR;

namespace ManageUsers.Application.Handlers.Patient.Queries.GetPatients;

public class GetPatientsQuery : ListPatientFilter, IBasePaginationFilter, IRequest<BaseListDto<GetPatientDto>>
{
    public int? Limit { get; init; }
    
    public int? Offset { get; init; }
}