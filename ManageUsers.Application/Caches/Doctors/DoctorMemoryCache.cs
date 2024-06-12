using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.DTOs.Doctor;
using ManageUsers.Domain.Shared;

namespace ManageUsers.Application.Caches.Doctors
{
    public class DoctorMemoryCache : BaseCache<Result<GetDoctorDto>>;
}
