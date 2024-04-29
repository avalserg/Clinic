using System.Security.Claims;
using ManageUsers.Application.Abstractions.Service;
using ManageUsers.Domain.Enums;

namespace ManageUsers.Api;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor) 
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Guid? CurrentUserId
    {
        get
        {
            string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return null;
            }

            return Guid.Parse(userId);
        }
    }

    

    public bool UserInRole(ApplicationUserRoles role)
    {
        return CurrentUserRole.Equals(role);
    }

    public ApplicationUserRoles[] CurrentUserRole =>
        _httpContextAccessor.HttpContext!.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c=>c.Value).Select(Enum.Parse<ApplicationUserRoles>).ToArray();

}