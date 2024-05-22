namespace Reviews.Domain.Exceptions.Base;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("Unauthorized")
    {
    }
}