using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageUsers.Domain.Enums;

namespace ManageUsers.Domain
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateBirthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? Avatar { get; set; }
        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


    }
}
