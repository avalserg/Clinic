using ManageUsers.Application.Abstractions.Mappings;
using ManageUsers.Domain.ValueObjects;

namespace ManageUsers.Application.DTOs.Patient
{
    public class UpdatePatientDto : IMapFrom<Domain.Patient>
    {
        public Guid Id { get; set; }
        public FullName FullName { get; set; }
        public DateTime DateBirthday { get; set; }
        public string Address { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public Guid ApplicationUserId { get; set; }

    }
}
