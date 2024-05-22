using MedicalCards.Domain.Primitives;

namespace MedicalCards.Domain
{
    public class MedicalCard:Entity
    {
        
        private MedicalCard(
            Guid id,
            Guid patientId
            ):base(id)
        {
            
            PatientId = patientId;
        }

        private MedicalCard(){ }
        
        public Guid PatientId { get;private set; }
        
        // public List<Prescription> Prescriptions { get; private set; }
        public List<Appointment> Appointments { get; private set; }
        public static MedicalCard Create(
            Guid id,
            Guid patientId
        )
        {
            //TODO check what card with patientId not exist 
            
            var medicalCard = new MedicalCard(
                id,
                patientId
            );
            
            //some  logic to create entity
            return medicalCard;
        }

    }
}
