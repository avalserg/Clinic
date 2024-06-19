using System.Linq.Expressions;

namespace MedicalCards.Application.Handlers.Prescription
{
    internal static class ListPrescriptionsWhere
    {
        public static Expression<Func<Domain.Prescription, bool>> Where(ListPrescriptionsFilter filter)
        {
            if (filter.DoctorId != Guid.Empty)
            {
                return prescriptions => prescriptions.DoctorId.Equals(filter.DoctorId);
            }
            if (filter.PatientId != Guid.Empty)
            {
                return prescriptions => prescriptions.PatientId.Equals(filter.PatientId);
            }
            var freeText = filter.FreeText?.Trim();
            return user => freeText == null || user.PatientId.Equals(freeText);
        }


    }
}
