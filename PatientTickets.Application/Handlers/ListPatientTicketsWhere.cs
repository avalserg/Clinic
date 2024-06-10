using PatientTickets.Domain.Entities;
using System.Linq.Expressions;

namespace PatientTickets.Application.Handlers;

internal static class ListPatientTicketsWhere
{
    public static Expression<Func<PatientTicket, bool>> Where(ListPatientTicketFilter filter)
    {

        if (filter.DoctorId != Guid.Empty)
        {
            return review => review.DoctorId.Equals(filter.DoctorId);
        }

        return review => filter.PatientId == Guid.Empty || review.PatientId.Equals(filter.PatientId);
    }
}