using AutoMapper;
using ManageUsers.Application.Abstractions.Mappings;
using ManageUsers.Application.DTOs.Patient;
using ManageUsers.Domain.ValueObjects;

namespace ManageUsers.Application.DTOs.Doctor
{
    public class GetDoctorDto : IMapFrom<Domain.Doctor>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }

        public DateTime DateBirthday { get; set; }
        public string Address { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public string? Photo { get; set; }
        public int Experience { get; set; }
        public string CabinetNumber { get; set; }
        public string Category { get; set; } = string.Empty;
        public Guid ApplicationUserId { get; set; }
        public string Speciality { get; set; }
        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Domain.Doctor, GetDoctorDto>()
                .ForMember(e => e.FirstName, r => r.MapFrom(u => u.FullName.FirstName))

                .ForMember(e => e.LastName, r => r.MapFrom(u => u.FullName.LastName))

                .ForMember(e => e.Patronymic, r => r.MapFrom(u => u.FullName.Patronymic));

        }

    }
}
