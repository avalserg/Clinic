using ManageUsers.Domain.Exceptions.Base;

namespace ManageUsers.Domain.Exceptions
{
    public class PatientNotFoundDomainException:NotFoundException
    {
        public PatientNotFoundDomainException(Guid patientId) : base($"The patient with the identifier {patientId} was not found")
        {
        }
    }
}
