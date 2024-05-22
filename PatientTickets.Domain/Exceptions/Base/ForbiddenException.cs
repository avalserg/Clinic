namespace PatientTickets.Domain.Exceptions.Base;

public class ForbiddenException : Exception
{
    public ForbiddenException() : base("Forbidden")
    {
    }
}