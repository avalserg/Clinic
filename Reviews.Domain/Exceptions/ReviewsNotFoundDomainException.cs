using Reviews.Domain.Entities;
using Reviews.Domain.Exceptions.Base;

namespace Reviews.Domain.Exceptions
{
    public sealed class ReviewsNotFoundDomainException:DomainException
    {
        public ReviewsNotFoundDomainException(Guid reviewId)
            : base($"The ${nameof(Review)} with the identifier {reviewId} was not found")
        {

        }
    }
}
