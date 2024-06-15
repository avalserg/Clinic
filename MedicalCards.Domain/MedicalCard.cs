using MedicalCards.Domain.Primitives;

namespace MedicalCards.Domain
{
    public class MedicalCard : Entity
    {

        private MedicalCard(
            Guid id,
            Guid patientId,
            string firstName,
            string lastName,
            string patronymic,
            DateTime dateBirthDay,
            string phoneNumber,
            string address

            ) : base(id)
        {

            PatientId = patientId;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            DateBirthday = dateBirthDay;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        private MedicalCard() { }

        public Guid PatientId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Patronymic { get; private set; }
        public DateTime DateBirthday { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }

        public List<Appointment> Appointments { get; private set; }
        public static MedicalCard Create(
            Guid id,
            Guid patientId,
            string firstName,
            string lastName,
            string patronymic,
            DateTime dateBirthDay,
            string phoneNumber,
            string address
        )
        {

            var medicalCard = new MedicalCard(
                id,
                patientId,
                firstName,
                lastName,
                patronymic,
                dateBirthDay,
                phoneNumber,
                address
            );

            //some  logic to create entity
            return medicalCard;
        }
        public void UpdateOwnerMedicalCardInfo(
            string firstName,
            string lastName,
            string patronymic,
            DateTime dateBirthDay,
            string phoneNumber,
            string address
        )
        {

            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            DateBirthday = dateBirthDay;
            PhoneNumber = phoneNumber;
            Address = address;

        }

    }
}
