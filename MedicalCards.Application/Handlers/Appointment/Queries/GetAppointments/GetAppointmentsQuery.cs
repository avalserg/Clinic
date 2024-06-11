using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.DTOs;
using MedicalCards.Application.DTOs.Appointment;

namespace MedicalCards.Application.Handlers.Appointment.Queries.GetAppointments
{
    public class GetAppointmentsQuery : ListAppointmentsFilter, IBasePaginationFilter, IQuery<BaseListDto<GetAppointmentDto>>
    {
        public int? Limit { get; init; }

        public int? Offset { get; init; }
    }
}
