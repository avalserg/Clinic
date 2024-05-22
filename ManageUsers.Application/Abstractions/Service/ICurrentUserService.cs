using ManageUsers.Domain.Enums;

namespace ManageUsers.Application.Abstractions.Service;

public interface ICurrentUserService
{
    public Guid? CurrentUserId { get; }
    
    public string CurrentUserRoleEnum { get; }

    public bool UserInRole(ApplicationUserRolesEnum roleEnum);
}