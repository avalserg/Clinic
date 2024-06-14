using PatientTickets.Application.Abstractions.Messaging;

namespace PatientTickets.Application.Handlers.Queries.GetBusyTimeWithTheDoctor
{
    public class GetBusyTimeWithTheDoctorQuery : IQuery<string[]>
    {
        public Guid DoctorId { get; init; }
        public DateTime DateAppointment { get; init; }
    }
}
