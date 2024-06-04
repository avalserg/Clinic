using AutoMapper;
using ManageUsers.Application.Abstractions.Mappings;
using ManageUsers.Application.DTOs.CurrentUser;
using ManageUsers.Domain;

namespace ManageUsers.Application.DTOs.ApplicationUser
{
    public class GetApplicationUserDto:IMapFrom<Domain.ApplicationUser>
    {
        public Guid ApplicationUserId { get; set; }
        public string Login { get; set; } = string.Empty;
        public ApplicationUserRole ApplicationUserRole { get; set; }

        //public void CreateMap(Profile profile)
        //{
        //    profile.CreateMap<Domain.ApplicationUser, GetApplicationUserDto>()
        //        .ForMember(e => e.ApplicationUserRole.Name, r => r.MapFrom(u => u.ApplicationUserRole.Name.ToUpper()));

        //}
    }
}
