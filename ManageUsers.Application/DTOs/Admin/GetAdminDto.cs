using AutoMapper;
using ManageUsers.Application.Abstractions.Mappings;
using ManageUsers.Application.DTOs.Doctor;
using ManageUsers.Domain;
using ManageUsers.Domain.ValueObjects;

namespace ManageUsers.Application.DTOs.Admin
{
    public class GetAdminDto:IMapFrom<Administrator>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public Guid ApplicationUserId { get; set; }
        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Administrator, GetAdminDto>()
                .ForMember(e => e.FirstName, r => r.MapFrom(u => u.FullName.FirstName))

                .ForMember(e => e.LastName, r => r.MapFrom(u => u.FullName.LastName))

                .ForMember(e => e.Patronymic, r => r.MapFrom(u => u.FullName.Patronymic));

        }
    }
}
