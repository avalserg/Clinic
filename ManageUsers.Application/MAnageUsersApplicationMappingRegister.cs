using ManageUsers.Application.BaseRealizations;

namespace ManageUsers.Application;

public class UsersApplicationMappingRegister : MappingRegister
{
    public UsersApplicationMappingRegister() : base(typeof(UsersApplicationMappingRegister).Assembly)
    {
    }
}
