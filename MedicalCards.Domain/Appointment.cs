using MedicalCards.Domain.Primitives;

namespace MedicalCards.Domain
{
    public class Appointment:Entity
    {
        private Appointment(
            Guid id,
            string descriptionEpicrisis,
            string descriptionAnamnesis,
            DateTime issuingTime,
            Guid medicalCardId,
            Guid doctorId,
            Guid patientId
        ) : base(id)
        {
            
            DescriptionEpicrisis = descriptionEpicrisis;
            DescriptionAnamnesis = descriptionAnamnesis;
            IssuingTime = issuingTime;
            MedicalCardId = medicalCardId;
            DoctorId = doctorId;
            PatientId = patientId;


        }
        private Appointment(){ }
       
        public Guid DoctorId { get; private set; }
        public Guid PatientId { get; private set; }
        // history disease with patient`s complains
        public string DescriptionEpicrisis { get; private set; }
        
        // methods research final diagnosis
        public string DescriptionAnamnesis { get; private set; }
        public DateTime IssuingTime { get; private set; }
        public string? PrescriptionId { get; private set; }
        public List<Prescription>? Prescriptions { get; private set; }
        public Guid MedicalCardId { get; private set; }
        // public MedicalCard MedicalCard { get; private set; }
        public static Appointment Create(
            Guid id,
            string descriptionEpicrisis,
            string descriptionAnamnesis,
            DateTime issuingTime,
            Guid medicalCardId,
            Guid doctorId,
            Guid patientId
        )
        {

            var appointment = new Appointment(
                id,
                descriptionEpicrisis,
                descriptionAnamnesis,
                issuingTime,
                medicalCardId,
                doctorId,
                patientId
            );

            //some  logic to create entity
            return appointment;
        }
    }
}
