namespace ManageUsers.Application.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("Unauthorized")
    {
    }
}