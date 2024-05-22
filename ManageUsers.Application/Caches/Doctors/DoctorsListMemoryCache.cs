using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.DTOs;
using ManageUsers.Application.DTOs.Doctor;

namespace ManageUsers.Application.Caches.Doctors
{
    public class DoctorsListMemoryCache : BaseCache<BaseListDto<GetDoctorDto>>;
    
}
