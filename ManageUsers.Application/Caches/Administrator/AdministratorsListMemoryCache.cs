using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.DTOs;
using ManageUsers.Application.DTOs.Admin;

namespace ManageUsers.Application.Caches.Administrator
{
    public class AdministratorsListMemoryCache : BaseCache<BaseListDto<GetAdminDto>>;
    
}
