using Reviews.Domain.Errors;
using Reviews.Domain.Primitives;
using Reviews.Domain.Shared;

namespace Reviews.Domain.Entities
{
    public class Review : Entity
    {

        private Review(
            Guid id,
            Guid patientId,
            string firstName,
            string lastName,
            string patronymic,
            string description
            ) : base(id)
        {
            CreatedDate = DateTime.Now;
            PatientId = patientId;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Description = description;
        }

        private Review() { }

        public DateTime CreatedDate { get; private set; }
        public Guid PatientId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Patronymic { get; private set; }
        public string Description { get; private set; }

        public static Result<Review> Create(
            Guid id,
            Guid patientId,
            string firstName,
            string lastName,
            string patronymic,
            string description
        )
        {

            if (string.IsNullOrWhiteSpace(description))
            {
                return Result.Failure<Review>(DomainErrors.Review.EmptyReviewDescription);
            }

            if (description.Length > ReviewDomainConst.MaxLengthDescription)
            {
                return Result.Failure<Review>(DomainErrors.Review.TooLongReviewDescription);
            }

            var review = new Review(
                id,
                patientId,
                firstName,
                lastName,
                patronymic,
                description
            );

            //some  logic to create entity
            return review;
        }

    }
}
