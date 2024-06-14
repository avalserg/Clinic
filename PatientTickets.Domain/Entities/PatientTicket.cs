using PatientTickets.Domain.Primitives;

namespace PatientTickets.Domain.Entities
{
    public class PatientTicket : Entity
    {

        private PatientTicket(
            Guid id,
            Guid patientId,
            DateTime dateAppointment,
            Guid doctorId,
            string doctorFirstName,
            string doctorLastName,
            string doctorPatronymic,
            string cabinetNumber,
            string doctorSpeciality
        ) : base(id)
        {
            DateAppointment = dateAppointment;
            DoctorId = doctorId;
            PatientId = patientId;
            // initial state when it was creating always false
            HasDoctorVisit = false;
            DoctorFirstName = doctorFirstName;
            DoctorLastName = doctorLastName;
            DoctorPatronymic = doctorPatronymic;
            CabinetNumber = cabinetNumber;
            DoctorSpeciality = doctorSpeciality;
        }

        private PatientTicket() { }

        public DateTime DateAppointment { get; private set; }
        public Guid DoctorId { get; private set; }
        public Guid PatientId { get; private set; }
        public bool HasDoctorVisit { get; private set; }
        public string DoctorFirstName { get; private set; }
        public string DoctorLastName { get; private set; }
        public string DoctorPatronymic { get; private set; }

        public string CabinetNumber { get; private set; }
        public string DoctorSpeciality { get; private set; }

        public static PatientTicket Create(
            Guid id,
            Guid patientId,
            DateTime dateAppointment,
            Guid doctorId,
            string doctorFirstName,
            string doctorLastName,
            string doctorPatronymic,
            string cabinetNumber,
            string doctorSpeciality
        )
        {


            var patientTicket = new PatientTicket(
                id,
                patientId,
                dateAppointment,
                doctorId,
                 doctorFirstName,

                doctorLastName,
                doctorPatronymic,
                cabinetNumber,
                doctorSpeciality
                );

            //some  logic to create entity
            return patientTicket;
        }

        public void UpdatePatientTicketHasVisit(
           bool hasVisit
        )
        {
            HasDoctorVisit = hasVisit;
        }

    }
}