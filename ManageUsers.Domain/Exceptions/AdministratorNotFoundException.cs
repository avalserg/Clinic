using ManageUsers.Domain.Exceptions.Base;

namespace ManageUsers.Domain.Exceptions
{
    public sealed class AdministratorNotFoundException:NotFoundException
    {
        public AdministratorNotFoundException(Guid administratorId)
            : base($"The administrator with the identifier {administratorId} was not found")
        {

        }
    }
}
