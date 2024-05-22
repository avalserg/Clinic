namespace Authorization.Application.Models;

public class GetApplicationUserDto
{
    public Guid ApplicationUserId { get; set; }
    
    public int ApplicationUserRole { get; set; } = default!;
    public string Login { get; set; } = default!;
    public GetApplicationUserDto(Guid applicationUserId, int role, string login)
    {
        ApplicationUserId = applicationUserId;
        ApplicationUserRole = role;
        Login = login;
    }

    public GetApplicationUserDto() { }
}