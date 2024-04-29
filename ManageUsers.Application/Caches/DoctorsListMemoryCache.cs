using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.DTOs;
using ManageUsers.Application.DTOs.Doctor;
using ManageUsers.Application.DTOs.Patient;

namespace ManageUsers.Application.Caches
{
    public class DoctorsListMemoryCache : BaseCache<BaseListDto<GetDoctorDto>>;
    
}
