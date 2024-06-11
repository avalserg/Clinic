using System.Linq.Expressions;

namespace MedicalCards.Application.Handlers.Prescription
{
    internal static class ListPrescriptionsWhere
    {
        public static Expression<Func<Domain.Prescription, bool>> Where(ListPrescriptionsFilter filter)
        {
            var freeText = filter.FreeText?.Trim();
            return user => freeText == null || user.PatientId.Equals(freeText);
        }
    }
}
