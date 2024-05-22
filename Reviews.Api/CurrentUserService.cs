using System.Security.Claims;
using PatientTickets.Domain.Enums;
using Reviews.Application.Abstractions.Service;

namespace Reviews.Api;

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

    

    public bool UserInRole(ApplicationUserRolesEnum roleEnum)
    {
        
        return CurrentUserRoleEnum.Equals(roleEnum.ToString());
    }

    //public ApplicationUserRolesEnum CurrentUserRoleEnum =>
    //    _httpContextAccessor.HttpContext!.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c=>c.Value).Select(Enum.Parse<ApplicationUserRolesEnum>);
    public string CurrentUserRoleEnum => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.Role).Value;
        

}