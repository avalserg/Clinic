namespace ManageUsers.Application.Exceptions;

public class BadOperationException : Exception
{
    public BadOperationException(string? message) : base(message)
    {
    }
}