using ManageUsers.Application.Abstractions.Mappings;
using ManageUsers.Domain.Enums;

namespace ManageUsers.Application.DTOs.ApplicationUser
{
    public class CreateApplicationUserDto : IMapFrom<Domain.ApplicationUser>
    {
        public Guid ApplicationUserId { get; set; } = default!;
        public string Login { get; set; } = default!;
        public int ApplicationUserRoleId { get; set; } = default!;
    }
}
