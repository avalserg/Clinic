using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageUsers.Domain.Enums;

namespace ManageUsers.Domain
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }=string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public DateTime DateBirthday { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Photo { get; set; }
        public int Experience { get; set; }
        public int? CabinetNumber { get; set; }
        public string Category { get; set; } = string.Empty;
        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


    }
}
