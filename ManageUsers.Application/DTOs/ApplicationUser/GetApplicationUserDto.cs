using AutoMapper;
using ManageUsers.Application.Abstractions.Mappings;
using ManageUsers.Domain;

namespace ManageUsers.Application.DTOs.ApplicationUser
{
    public class GetApplicationUserDto:IMapFrom<Domain.ApplicationUser>
    {
        public Guid ApplicationUserId { get; set; }
        public string Login { get; set; } = string.Empty;
        public ApplicationUserRole ApplicationUserRole { get; set; }
      
    }
}
