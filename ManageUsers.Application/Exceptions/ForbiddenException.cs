namespace ManageUsers.Application.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException() : base("Forbidden")
    {
    }
}