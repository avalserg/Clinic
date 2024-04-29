using ManageUsers.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageUsers.Application.Abstractions.Mappings;
using ManageUsers.Domain;

namespace ManageUsers.Application.DTOs.Patient
{
    public class GetPatientDto : IMapFrom<Domain.Patient>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateBirthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? Avatar { get; set; }
        public Guid ApplicationUserId { get; set; }
        
    }
}
