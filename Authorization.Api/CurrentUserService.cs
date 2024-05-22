using System.Security.Claims;
using Authorization.Application.Abstractions.Service;

namespace Authorization.Api;

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

    

    public bool UserInRole(string role)
    {
        return CurrentUserRole.Contains(role);
    }

    public string CurrentUserRole =>
        _httpContextAccessor.HttpContext!.User.Claims.Where(c => c.Type == ClaimTypes.Role).ToString()!;

}