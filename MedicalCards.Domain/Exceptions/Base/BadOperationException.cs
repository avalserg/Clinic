namespace MedicalCards.Domain.Exceptions.Base;

public class BadOperationException : Exception
{
    public BadOperationException(string? message) : base(message)
    {
    }
}