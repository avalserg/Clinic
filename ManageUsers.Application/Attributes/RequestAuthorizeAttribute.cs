using ManageUsers.Domain.Enums;

namespace ManageUsers.Application.Attributes;

public class RequestAuthorizeAttribute : Attribute
{
    private static ApplicationUserRoles[]? _roles;
    public ApplicationUserRoles[]? Roles { get; } = _roles;

    public RequestAuthorizeAttribute(ApplicationUserRoles[]? roles = null)
    {
        _roles = roles;
    }
}