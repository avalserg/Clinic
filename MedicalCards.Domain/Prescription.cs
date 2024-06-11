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
            Guid patientId
        ) : base(id)
        {
            MedicineName = medicineName;
            ReleaseForm = releaseForm;
            Amount = amount;
            IssuingTime = issuingTime;
            AppointmentId = appointmentId;
            DoctorId = doctorId;
            PatientId = patientId;


        }

        private Prescription() { }
        public string MedicineName { get; private set; }
        public string ReleaseForm { get; private set; }
        public string Amount { get; private set; }
        public DateTime IssuingTime { get; private set; }

        public Guid DoctorId { get; private set; }
        public Guid PatientId { get; private set; }
        public Guid AppointmentId { get; private set; }
        public static Prescription Create(
            Guid id,
            string medicineName,
            string releaseForm,
            string amount,
            DateTime issuingTime,
            Guid appointmentId,
            Guid doctorId,
            Guid patientId
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
                patientId
            );

            //some  logic to create entity
            return prescription;
        }
    }
}
