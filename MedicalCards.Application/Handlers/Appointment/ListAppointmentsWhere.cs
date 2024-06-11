using System.Linq.Expressions;

namespace MedicalCards.Application.Handlers.Appointment;

internal static class ListAppointmentsWhere
{
    public static Expression<Func<Domain.Appointment, bool>> Where(ListAppointmentsFilter filter)
    {
        var freeText = filter.FreeText?.Trim();
        return user => freeText == null || user.PatientId.Equals(freeText);
    }
}