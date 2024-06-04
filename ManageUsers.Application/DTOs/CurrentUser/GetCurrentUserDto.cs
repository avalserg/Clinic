using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageUsers.Application.Abstractions.Mappings;
using ManageUsers.Domain;

namespace ManageUsers.Application.DTOs.CurrentUser
{
    public class GetCurrentUserDto : IMapFrom<Domain.ApplicationUser>
    {
        public Guid ApplicationUserId { get; set; }

        public string Login { get; set; } = default!;
        public ApplicationUserRole ApplicationUserRole { get; set; } = default!;


        //public void CreateMap(Profile profile)
        //{
        //    profile.CreateMap<Domain.ApplicationUser, GetCurrentUserDto>()
        //        .ForMember(e => e.ApplicationUserRole, r => r.MapFrom(u => u.ApplicationUserRole.Name.ToUpper()));

        //}
    }
}
