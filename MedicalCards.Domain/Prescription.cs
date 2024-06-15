using MedicalCards.Domain.Primitives;

namespace MedicalCards.Domain
{
    public class Prescription : Entity
    {
        private Prescription(
            Guid id,
            string medicineName,
            string releaseForm,
            string amount,
            DateTime issuingTime,
            Guid appointmentId,
            Guid doctorId,
            string doctorFirstName,
            string doctorLastName,
            string doctorPatronymic,
            Guid patientId,
            string patientFirstName,
            string patientLastName,
            string patientPatronymic
        ) : base(id)
        {
            MedicineName = medicineName;
            ReleaseForm = releaseForm;
            Amount = amount;
            IssuingTime = issuingTime;
            AppointmentId = appointmentId;
            DoctorId = doctorId;
            PatientId = patientId;
            DoctorFirstName = doctorFirstName;
            DoctorLastName = doctorLastName;
            DoctorPatronymic = doctorPatronymic;
            PatientFirstName = patientFirstName;
            PatientLastName = patientLastName;
            PatientPatronymic = patientPatronymic;

        }

        private Prescription() { }
        public string MedicineName { get; private set; }
        public string ReleaseForm { get; private set; }
        public string Amount { get; private set; }
        public DateTime IssuingTime { get; private set; }

        public Guid DoctorId { get; private set; }
        public string DoctorFirstName { get; private set; }
        public string DoctorLastName { get; private set; }
        public string DoctorPatronymic { get; private set; }
        public Guid PatientId { get; private set; }
        public string PatientFirstName { get; private set; }
        public string PatientLastName { get; private set; }
        public string PatientPatronymic { get; private set; }
        public Guid AppointmentId { get; private set; }
        public static Prescription Create(
            Guid id,
            string medicineName,
            string releaseForm,
            string amount,
            DateTime issuingTime,
            Guid appointmentId,
            Guid doctorId,
            string doctorFirstName,
            string doctorLastName,
            string doctorPatronymic,
            Guid patientId,
            string patientFirstName,
            string patientLastName,
            string patientPatronymic
        )
        {

            var prescription = new Prescription(
                id,
                medicineName,
                releaseForm,
                amount,
                issuingTime,
                appointmentId,
                doctorId,
                doctorFirstName,
                doctorLastName,
                doctorPatronymic,
                patientId,
                patientFirstName,
                patientLastName,
                patientPatronymic
            );

            //some  logic to create entity
            return prescription;
        }
    }
}
