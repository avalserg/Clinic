using MedicalCards.Application.Abstractions.Service;
using MedicalCards.Domain.Enums;
using System.Security.Claims;

namespace MedicalCards.Api;

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

        int role = (int)roleEnum;
        return CurrentUserRoleEnum.Equals(role.ToString());
    }

    public string CurrentUserRoleEnum => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;


}