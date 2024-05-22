using ManageUsers.Domain.Exceptions.Base;

namespace ManageUsers.Domain.Exceptions
{
    public sealed class AdministratorNotFoundDomainException:DomainException
    {
        public AdministratorNotFoundDomainException(Guid administratorId)
            : base($"The administrator with the identifier {administratorId} was not found")
        {

        }
    }
}
