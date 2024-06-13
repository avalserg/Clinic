using Reviews.Domain.Shared;

namespace Reviews.Domain.Errors;

public static class DomainErrors
{

    public static class Review
    {
        public static readonly Error EmptyReviewDescription = new(
              "Review.EmptyReviewDescription",
              $"The {nameof(Entities.Review)} is empty");

        public static readonly Error TooLongReviewDescription = new(
              "Review.TooLongReviewDescription",
              $"The {nameof(Entities.Review)} has more than {ReviewDomainConst.MaxLengthDescription} characters");

        public static readonly Func<Guid, Error> NotFound = reviewId => new Error(
            $"{nameof(Entities.Review)}.NotFound",
            $"The {nameof(Entities.Review)} with the identifier {reviewId} was not found.");
        public static readonly Func<Guid, Error> CreatorReviewNotFound = patientId => new Error(
            $"{nameof(Entities.Review)}.CreatorReviewNotFound",
            $"The {nameof(Entities.Review)} with the identifier {patientId} was not found.");
    }

}
