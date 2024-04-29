using ManageUsers.Application.DTOs;
using ManageUsers.Application.DTOs.Doctor;
using ManageUsers.Application.DTOs.Patient;
using MediatR;

namespace ManageUsers.Application.Handlers.Doctor.Queries.GetDoctors;

public class GetDoctorsQuery : ListDoctorsFilter, IBasePaginationFilter, IRequest<BaseListDto<GetDoctorDto>>
{
    public int? Limit { get; init; }
    
    public int? Offset { get; init; }
}