using Authorization.Application.Abstractions.Mappings;
using Authorization.Domain;
using AutoMapper;

namespace Authorization.Application.DTOs;

public class GetUserDto : IMapFrom<ApplicationUser>
{
    public int ApplicationUserId { get; set; }
    
    public string Login { get; set; } = default!;

    public int RoleId { get; set; }

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<ApplicationUser, GetUserDto>()
            .ForMember(e => e.RoleId, r =>
                r.MapFrom(u => u.ApplicationUserRole!.ApplicationUserRoleId));
    }
}