namespace Authorization.Application.Models;

public class GetApplicationUserDto
{
    public Guid ApplicationUserId { get; set; }

    public ApplicationUserRole ApplicationUserRole { get; set; } = default!;
    public string Login { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public GetApplicationUserDto(Guid applicationUserId, ApplicationUserRole role, string login, string passwordHash)
    {
        ApplicationUserId = applicationUserId;
        ApplicationUserRole = role;
        ApplicationUserRole.Name = role.Name.ToUpper();
        Login = login;
        PasswordHash = passwordHash;
    }

    public GetApplicationUserDto() { }
}
public class ApplicationUserRole
{
    public int ApplicationUserRoleId { get; set; }

    public string Name { get; set; } = default!;

}