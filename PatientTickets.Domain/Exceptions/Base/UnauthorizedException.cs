namespace PatientTickets.Domain.Exceptions.Base;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("Unauthorized")
    {
    }
}