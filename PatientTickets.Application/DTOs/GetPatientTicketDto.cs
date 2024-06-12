using PatientTickets.Application.Abstractions.Mappings;
using PatientTickets.Domain.Entities;

namespace PatientTickets.Application.DTOs
{
    public class GetPatientTicketDto : IMapFrom<PatientTicket>
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public DateTime DateAppointment { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorPatronymic { get; set; }

        public string CabinetNumber { get; set; }
        public string DoctorSpeciality { get; set; }
        public bool HasDoctorVisit { get; set; }
    }
}
