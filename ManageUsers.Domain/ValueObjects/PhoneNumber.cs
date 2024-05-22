using System.Text.RegularExpressions;
using ManageUsers.Domain.Errors;
using ManageUsers.Domain.Primitives;
using ManageUsers.Domain.Shared;

namespace ManageUsers.Domain.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    private const string PhoneRegex = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";

    private PhoneNumber(string phoneNumber)
    {
        Value = phoneNumber;
    }

    private PhoneNumber()
    {
    }

    public string Value { get; private set; }

    public static Result<PhoneNumber> Create(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumberDomainErrors.Empty);
        }

        if (Regex.IsMatch(phoneNumber, PhoneRegex)==false)
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumberDomainErrors.InvalidFormat);
        }

        return new PhoneNumber(phoneNumber);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}