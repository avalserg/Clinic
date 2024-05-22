using ManageUsers.Domain.Errors;
using ManageUsers.Domain.Primitives;
using ManageUsers.Domain.Shared;

namespace ManageUsers.Domain.ValueObjects;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 50;

    private FirstName(string value)
    {
        Value = value;
    }

    private FirstName() { }

    public string Value { get; private set; }

    public static Result<FirstName> Create(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result.Failure<FirstName>(DomainErrors.FirstNameDomainErrors.Empty);
        }

        if (firstName.Length > MaxLength)
        {
            return Result.Failure<FirstName>(DomainErrors.FirstNameDomainErrors.TooLong);
        }

        return new FirstName(firstName);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
