using ManageUsers.Domain.Enums;

namespace ManageUsers.Application.Abstractions.Service;

public interface ICurrentUserService
{
    public Guid? CurrentUserId { get; }
    
    public ApplicationUserRoles[] CurrentUserRole { get; }

    public bool UserInRole(ApplicationUserRoles role);
}