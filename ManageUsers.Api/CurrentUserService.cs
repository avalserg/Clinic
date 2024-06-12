using ManageUsers.Application.Abstractions.Service;
using ManageUsers.Domain.Enums;
using System.Security.Claims;

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
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return null;
            }
            return Guid.Parse(userId);
        }
    }

    public bool UserInRole(ApplicationUserRolesEnum roleEnum)
    {
        var role = (int)roleEnum;
        return CurrentUserRoleEnum.Equals(role.ToString());
    }

    public string CurrentUserRoleEnum => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;


}