using PatientTickets.Domain.Exceptions.Base;

namespace PatientTickets.Domain.Exceptions
{
    public sealed class PatientTicketNotFoundDomainException:DomainException
    {
        public PatientTicketNotFoundDomainException(Guid administratorId)
            : base($"The administrator with the identifier {administratorId} was not found")
        {

        }
    }
}
