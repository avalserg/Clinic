using Authorization.Application.Abstractions.Mappings;
using Authorization.Domain;
using AutoMapper;

namespace Authorization.Application.DTOs;

public class GetUserDto : IMapFrom<ApplicationUser>
{
    public Guid ApplicationUserId { get; set; }
    
    public string Login { get; set; } = default!;
    public string ApplicationUserRole { get; set; }=default!;
    

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<ApplicationUser, GetUserDto>()
            .ForMember(e => e.ApplicationUserRole, r => r.MapFrom(u => u.ApplicationUserRole.Name.ToUpper()));

    }
}