using AutoMapper;
using ManageUsers.Application.Abstractions.Mappings;
using ManageUsers.Domain.ValueObjects;

namespace ManageUsers.Application.DTOs.Patient
{
    public class GetPatientDto : IMapFrom<Domain.Patient>
    {
        public Guid Id { get; set; }
        // public FullName FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateBirthday { get; set; }
        public string Address { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public string PassportNumber { get; set; }
        public string? Avatar { get; set; }
        public Guid ApplicationUserId { get; set; }
        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Domain.Patient, GetPatientDto>()
                .ForMember(e => e.FirstName, r => r.MapFrom(u => u.FullName.FirstName)) 
            
                .ForMember(e => e.LastName, r => r.MapFrom(u => u.FullName.LastName))
            
                .ForMember(e => e.Patronymic, r => r.MapFrom(u => u.FullName.Patronymic));

        }
    }
}
