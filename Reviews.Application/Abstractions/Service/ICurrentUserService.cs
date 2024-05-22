using Reviews.Domain.Enums;

namespace Reviews.Application.Abstractions.Service;

public interface ICurrentUserService
{
    public Guid? CurrentUserId { get; }
    
    public string CurrentUserRoleEnum { get; }

    public bool UserInRole(ApplicationUserRolesEnum roleEnum);
}