using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.DTOs.Appointment;

namespace MedicalCards.Application.Handlers.Appointment.Queries.GetAppointment
{
    public class GetAppointmentQuery : IQuery<GetAppointmentDto>
    {
        public Guid Id { get; init; } = default!;
    }
}
