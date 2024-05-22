using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.DTOs.ApplicationUser;

namespace ManageUsers.Application.Caches.ApplicationUserMemoryCache
{
    public class ApplicationUserMemoryCache : BaseCache<GetApplicationUserDto>;
    
}
