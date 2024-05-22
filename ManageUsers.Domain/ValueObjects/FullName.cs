using ManageUsers.Domain.Errors;
using ManageUsers.Domain.Primitives;
using ManageUsers.Domain.Shared;

namespace ManageUsers.Domain.ValueObjects
{
    public class FullName:ValueObject
    {
        public const int MaxLength = 50;

        private FullName(string firstName, string lastName, string patronymic)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
        }

        private FullName()
        {
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Patronymic { get; private set; }

        public static Result<FullName> Create(string firstName, string lastName, string patronymic)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Result.Failure<FullName>(DomainErrors.FirstNameDomainErrors.Empty);
            }

            if (firstName.Length > MaxLength)
            {
                return Result.Failure<FullName>(DomainErrors.FirstNameDomainErrors.TooLong);
            } 

            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result.Failure<FullName>(DomainErrors.LastNameDomainErrors.Empty);
            }

            if (lastName.Length > MaxLength)
            {
                return Result.Failure<FullName>(DomainErrors.LastNameDomainErrors.TooLong);
            }
            if (string.IsNullOrWhiteSpace(patronymic))
            {
                return Result.Failure<FullName>(DomainErrors.PatronymicDomainErrors.Empty);
            }

            if (patronymic.Length > MaxLength)
            {
                return Result.Failure<FullName>(DomainErrors.PatronymicDomainErrors.TooLong);
            }

            return new FullName(firstName,lastName,patronymic);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
            yield return Patronymic;
        }
    }
}
