using System.Linq.Expressions;
using Reviews.Domain.Entities;

namespace Reviews.Application.Handlers;

internal static class ListReviewsWhere
{
    public static Expression<Func<Review, bool>> Where(ListReviewFilter filter)
    {
        var freeText = filter.PatientId;
        return review => freeText== Guid.Empty || review.PatientId.Equals(freeText);
    }
}