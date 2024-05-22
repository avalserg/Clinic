using PatientTickets.Domain.Primitives;

namespace PatientTickets.Domain.Entities
{
    public class PatientTicket : Entity
    {

        private PatientTicket(
            Guid id,
            DateTime dateAppointment,
            Guid doctorId,
            Guid patientId
        ) : base(id)
        {
            DateAppointment = dateAppointment;
            DoctorId = doctorId;
            PatientId = patientId;
            // initial state while creating always false
            HasDoctorVisit = false;
        }

        private PatientTicket() { }

        public DateTime DateAppointment { get; private set; }
        public Guid DoctorId { get; private set; }
        public Guid PatientId { get; private set; }
        public bool HasDoctorVisit { get; private set; }

        public static PatientTicket Create(
            Guid id,
            DateTime dateAppointment,
            Guid patientId,
            Guid doctorId
        )
        {
           
            
            var patientTicket = new PatientTicket(
                id,
                dateAppointment,
                patientId,
                doctorId
                );

            //some  logic to create entity
            return patientTicket;
        }

    }
}