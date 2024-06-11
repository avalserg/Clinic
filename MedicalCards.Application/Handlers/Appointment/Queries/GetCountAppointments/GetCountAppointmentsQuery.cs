using MediatR;

namespace MedicalCards.Application.Handlers.Appointment.Queries.GetCountAppointments
{
    public class GetCountAppointmentsQuery : ListAppointmentsFilter, IRequest<int>
    {
    }
}
