using ManageUsers.Application.Abstractions.Mappings;
using ManageUsers.Domain.ValueObjects;

namespace ManageUsers.Application.DTOs.Doctor
{
    public class GetDoctorDto : IMapFrom<Domain.Doctor>
    {
        public Guid Id { get; set; }
        public FullName FullName { get; set; }
       
        public DateTime DateBirthday { get; set; }
        public string Address { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public string? Photo { get; set; }
        public int Experience { get; set; }
        public int? CabinetNumber { get; set; }
        public string Category { get; set; } = string.Empty;
        public Guid ApplicationUserId { get; set; }


    }
}
