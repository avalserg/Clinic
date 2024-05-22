using MedicalCards.Domain.Enums;

namespace MedicalCards.Application.Abstractions.Service;

public interface ICurrentUserService
{
    public Guid? CurrentUserId { get; }
    
    public string CurrentUserRoleEnum { get; }

    public bool UserInRole(ApplicationUserRolesEnum roleEnum);
}