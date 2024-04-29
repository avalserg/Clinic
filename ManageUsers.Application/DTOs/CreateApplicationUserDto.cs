using ManageUsers.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageUsers.Application.Abstractions.Mappings;
using ManageUsers.Domain;

namespace ManageUsers.Application.DTOs
{
    public class CreateApplicationUserDto : IMapFrom<ApplicationUser>
    {
        public Guid ApplicationUserId { get; set; } = default!;
        public string Login { get; set; } = default!;
        public ApplicationUserRoles ApplicationUserRole { get; set; } = default!;
    }
}
