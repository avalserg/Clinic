using PatientTickets.Domain.Enums;

namespace PatientTickets.Application.Abstractions.Service;

public interface ICurrentUserService
{
    public Guid? CurrentUserId { get; }
    
    public string CurrentUserRoleEnum { get; }

    public bool UserInRole(ApplicationUserRolesEnum roleEnum);
}