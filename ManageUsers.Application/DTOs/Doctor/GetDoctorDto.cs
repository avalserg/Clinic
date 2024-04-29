using ManageUsers.Application.Abstractions.Mappings;

namespace ManageUsers.Application.DTOs.Doctor
{
    public class GetDoctorDto : IMapFrom<Domain.Doctor>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateBirthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? Photo { get; set; }
        public int Experience { get; set; }
        public int? CabinetNumber { get; set; }
        public string Category { get; set; } = string.Empty;
        public Guid ApplicationUserId { get; set; }


    }
}
