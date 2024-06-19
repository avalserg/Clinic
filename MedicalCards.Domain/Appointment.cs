using MedicalCards.Domain.Primitives;

namespace MedicalCards.Domain
{
    public class Appointment : Entity
    {
        private Appointment(
            Guid id,
            Guid medicalCardId,
            string descriptionEpicrisis,
            string descriptionAnamnesis,
            Guid doctorId,
            string doctorFirstName,
            string doctorLastName,
            string doctorPatronymic,
            string speciality,
            Guid patientId,
            string patientFirstName,
            string patientLastName,
            string patientPatronymic

        ) : base(id)
        {

            DescriptionEpicrisis = descriptionEpicrisis;
            DescriptionAnamnesis = descriptionAnamnesis;
            DoctorFirstName = doctorFirstName;
            DoctorLastName = doctorLastName;
            DoctorPatronymic = doctorPatronymic;
            Speciality = speciality;
            IssuingTime = DateTime.Now;
            MedicalCardId = medicalCardId;
            DoctorId = doctorId;
            PatientFirstName = patientFirstName;
            PatientLastName = patientLastName;
            PatientPatronymic = patientPatronymic;
            PatientId = patientId;
            // always false after created
            HasPrescription = false;


        }
        private Appointment() { }

        public Guid DoctorId { get; private set; }
        public Guid PatientId { get; private set; }
        // history disease with patient`s complains
        public string DescriptionEpicrisis { get; private set; }

        // methods research final diagnosis
        public string DescriptionAnamnesis { get; private set; }
        public string DoctorFirstName { get; private set; }
        public string DoctorLastName { get; private set; }
        public string DoctorPatronymic { get; private set; }
        public string PatientFirstName { get; private set; }
        public string PatientLastName { get; private set; }
        public string PatientPatronymic { get; private set; }
        public bool HasPrescription { get; private set; }

        public string Speciality { get; private set; }
        public DateTime IssuingTime { get; private set; }
        public string? PrescriptionId { get; private set; }
        public List<Prescription>? Prescriptions { get; private set; }
        public Guid MedicalCardId { get; private set; }

        public static Appointment Create(
            Guid id,
            string descriptionEpicrisis,
            string descriptionAnamnesis,
            string doctorFirstName,
            string doctorLastName,
            string doctorPatronymic,
            string speciality,

            Guid medicalCardId,
            Guid doctorId,
            Guid patientId,
            string patientFirstName,
            string patientLastName,
            string patientPatronymic
        )
        {

            var appointment = new Appointment(
                id,
                medicalCardId,
                descriptionEpicrisis,
                descriptionAnamnesis,
                doctorId,
                doctorFirstName,
                doctorLastName,
                doctorPatronymic,
                speciality,
                patientId,
                patientFirstName,
                patientLastName,
                patientPatronymic
            );

            //some  logic to create entity
            return appointment;
        }

        public void UpdateAppointmentHasPrescription(
            bool hasPrescription,
            string prescriptionId
        )
        {
            HasPrescription = hasPrescription;
            PrescriptionId = prescriptionId;
        }

    }
}
