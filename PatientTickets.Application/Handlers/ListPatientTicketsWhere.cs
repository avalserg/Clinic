using PatientTickets.Domain.Entities;
using System.Linq.Expressions;

namespace PatientTickets.Application.Handlers;

internal static class ListPatientTicketsWhere
{
    public static Expression<Func<PatientTicket, bool>> Where(ListPatientTicketFilter filter)
    {

        if (filter.DoctorId != Guid.Empty)
        {
            if (filter.DateAppointment == default)
            {
                return patientTickets => patientTickets.DoctorId.Equals(filter.DoctorId);

            }
            return patientTickets => patientTickets.DoctorId.Equals(filter.DoctorId) && patientTickets.DateAppointment.Equals(filter.DateAppointment);
        }

        return patientTickets => filter.PatientId == Guid.Empty || patientTickets.PatientId.Equals(filter.PatientId);
    }
}