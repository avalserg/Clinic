using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageUsers.Application.Abstractions.Mappings;
using ManageUsers.Domain.Enums;
using ManageUsers.Domain.Errors;

namespace ManageUsers.Application.DTOs.ApplicationUser
{
    public class GetApplicationUserDto:IMapFrom<Domain.ApplicationUser>
    {
        public Guid ApplicationUserId { get; set; }

        public string Login { get; set; } = string.Empty;
        
        public ApplicationUserRolesEnum ApplicationUserRole { get; set; }
    }
}
