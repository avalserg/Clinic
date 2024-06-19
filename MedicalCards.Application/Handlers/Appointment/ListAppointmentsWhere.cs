using System.Linq.Expressions;

namespace MedicalCards.Application.Handlers.Appointment;

internal static class ListAppointmentsWhere
{
    public static Expression<Func<Domain.Appointment, bool>> Where(ListAppointmentsFilter filter)
    {
        if (filter.DoctorId != Guid.Empty)
        {
            return appointments => appointments.DoctorId.Equals(filter.DoctorId);
        }
        if (filter.PatientId != Guid.Empty)
        {
            return appointments => appointments.PatientId.Equals(filter.PatientId);
        }
        var freeText = filter.FreeText?.Trim();
        return user => freeText == null || user.PatientId.Equals(freeText);
    }
}