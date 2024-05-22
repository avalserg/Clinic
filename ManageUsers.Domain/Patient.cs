using ManageUsers.Domain.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageUsers.Domain.Enums;
using ManageUsers.Domain.ValueObjects;

namespace ManageUsers.Domain
{
    public class Patient:Entity
    {
        
        private Patient(
            Guid id,
            FullName fullName,
            DateTime dateBirthday,
            string address,
            PhoneNumber phoneNumber,
            string? avatar,
            Guid applicationUserId
            ):base(id)
        {
            
            FullName = fullName;
            DateBirthday = dateBirthday;
            Address = address;
            PhoneNumber = phoneNumber;
            Avatar = avatar;
            ApplicationUserId = applicationUserId;
        }

        private Patient()
        {
            
        }
        //public FirstNameDomainErrors FirstNameDomainErrors { get; }
        //public LastNameDomainErrors LastNameDomainErrors { get; }
        //public string PatronymicDomainErrors { get; }
        public FullName FullName { get; private set; }
        public DateTime DateBirthday { get; private set; }
        public string Address { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public string? Avatar { get; private set; }
        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId { get; private set; }

        public ApplicationUser? ApplicationUser { get; private set; }
        public static Patient Create(
            Guid id,
            FullName fullName,
            DateTime dateBirthday,
            string address,
            PhoneNumber phoneNumber,
            string? avatar,
            Guid applicationUserId
        )
        {
            var patient = new Patient(
                id,
              
                fullName,
                dateBirthday,
                address,
                phoneNumber,
                avatar,
                applicationUserId
            );
            
            //some  logic to create entity
            return patient;
        }

    }
}
