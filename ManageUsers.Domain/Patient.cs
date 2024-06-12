using ManageUsers.Domain.Primitives;
using ManageUsers.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageUsers.Domain
{
    public class Patient : Entity
    {

        private Patient(
            Guid id,
            FullName fullName,
            DateTime dateBirthday,
            string address,
            PhoneNumber phoneNumber,
            string passportNumber,
            string? avatar,
            Guid applicationUserId
            ) : base(id)
        {

            FullName = fullName;
            DateBirthday = dateBirthday;
            Address = address;
            PhoneNumber = phoneNumber;
            Avatar = avatar;
            PassportNumber = passportNumber;
            ApplicationUserId = applicationUserId;
        }

        private Patient()
        {

        }

        public FullName FullName { get; private set; }
        public DateTime DateBirthday { get; private set; }
        public string Address { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public string PassportNumber { get; private set; }
        // Mark patient as deleted entity
        // public bool IsDeleted { get; private set; }
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
            string passportNumber,
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
                passportNumber,
                avatar,
                applicationUserId
            );

            //some  logic to create entity
            return patient;
        }
        public void Update(
            FullName fullName,
            DateTime dateBirthday,
            string address,
            PhoneNumber phoneNumber,
            string passportNumber,
            string? avatar

        )
        {

            FullName = fullName;
            DateBirthday = dateBirthday;
            Address = address;
            PhoneNumber = phoneNumber;
            PassportNumber = passportNumber;
            Avatar = avatar;

        }
        public void UpdateAvatar(
            string? avatar
        )
        {
            Avatar = avatar;
        }

    }
}
