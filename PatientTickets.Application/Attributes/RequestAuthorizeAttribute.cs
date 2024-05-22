using PatientTickets.Domain.Enums;

namespace PatientTickets.Application.Attributes;

public class RequestAuthorizeAttribute : Attribute
{
    private static ApplicationUserRolesEnum[]? _roles;
    public ApplicationUserRolesEnum[]? Roles { get; } = _roles;

    public RequestAuthorizeAttribute(ApplicationUserRolesEnum[]? roles = null)
    {
        _roles = roles;
    }
}