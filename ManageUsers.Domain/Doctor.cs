using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageUsers.Domain.Enums;
using ManageUsers.Domain.Primitives;
using ManageUsers.Domain.ValueObjects;

namespace ManageUsers.Domain
{
    public class Doctor:Entity
    {
        private Doctor(
            Guid id,
           
            FullName fullName,
            DateTime dateBirthday,
            string address,
            PhoneNumber phoneNumber,
            string? photo,
            int experience ,
            string cabinetNumber, 
            string category , 
            Guid applicationUserId
        ) : base(id)
        {


            FullName = fullName;
            DateBirthday = dateBirthday;
            Address = address;
            PhoneNumber = phoneNumber;
            Photo = photo;
            Experience = experience;
            CabinetNumber = cabinetNumber;
            Category = category;
            ApplicationUserId = applicationUserId;
        }

        public Doctor()
        {
            
        }
        
        public FullName FullName { get; private set; }
        public DateTime DateBirthday { get; private set; }
        public string Address { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public string? Photo { get; private set; }
        public int Experience { get; private set; }
        public string CabinetNumber { get; private set; }
        public string Category { get; private set; }
        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId { get; private set; }
        public ApplicationUser? ApplicationUser { get; private set; }

        public static Doctor Create(
            Guid id,
            FullName fullName,
            DateTime dateBirthday,
            string address,
            PhoneNumber phoneNumber,
            string? photo,
            int experience,
            string cabinetNumber,
            string category,
            Guid applicationUserId
        )
        {
            var doctor = new Doctor(
                id,
               fullName,
                dateBirthday,
                address,
                phoneNumber,
                photo,
                experience,
                cabinetNumber,
                category,
                applicationUserId
            );

            //some  logic to create entity
            return doctor;
        }
        public void Update(
            FullName fullName,
            DateTime dateBirthday,
            string address,
            PhoneNumber phoneNumber,
            int experience,
            string cabinetNumber,
            string category

        )
        {
            FullName = fullName;
            DateBirthday = dateBirthday;
            Address = address;
            PhoneNumber = phoneNumber;
            Experience = experience;
            CabinetNumber = cabinetNumber;
            Category = category;

        }
        public void UpdatePhoto(

            string? photo

        )
        {

            Photo = photo;

        }
    }
}
