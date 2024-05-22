namespace Authorization.Application.Abstractions.Service;

public interface ICurrentUserService
{
    public Guid? CurrentUserId { get; }
    
    public string CurrentUserRole { get; }

    public bool UserInRole(string role);
}