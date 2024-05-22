using MedicalCards.Domain.Exceptions.Base;

namespace MedicalCards.Domain.Exceptions
{
    public sealed class PatientTicketNotFoundDomainException:DomainException
    {
        public PatientTicketNotFoundDomainException(Guid administratorId)
            : base($"The administrator with the identifier {administratorId} was not found")
        {

        }
    }
}
