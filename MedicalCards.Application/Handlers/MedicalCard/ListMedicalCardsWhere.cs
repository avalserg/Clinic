using System.Linq.Expressions;

namespace MedicalCards.Application.Handlers.MedicalCard;

internal static class ListMedicalCardsWhere
{
    public static Expression<Func<Domain.MedicalCard, bool>> Where(ListMedicalCardsFilter filter)
    {
        var freeText = filter.FreeText?.Trim();
        return user => freeText == null || user.PatientId.Equals(freeText);
    }
}