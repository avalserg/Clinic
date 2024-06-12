using System.ComponentModel.DataAnnotations;

namespace ManageUsers.Domain;

public class ApplicationUser
{

    private ApplicationUser(
        Guid applicationUserId,
        string login,
        string passwordHash,
        int applicationUserRoleId)
    {
        ApplicationUserId = applicationUserId;
        Login = login;
        PasswordHash = passwordHash;
        ApplicationUserRoleId = applicationUserRoleId;
    }

    private ApplicationUser() { }
    [Key]
    public Guid ApplicationUserId { get; private set; }
    public int ApplicationUserRoleId { get; private set; }

    public string Login { get; private set; }

    public string PasswordHash { get; private set; }

    // Mark app user as deleted entity
    // public bool IsDeleted { get; private set; }

    public ApplicationUserRole ApplicationUserRole { get; private set; } = default!;


    public static ApplicationUser Create(
        Guid applicationUserId,
        string login,
        string passwordHash,
        int applicationUserRoleId
    )
    {
        var applicationUser = new ApplicationUser(
            applicationUserId,
            login,
            passwordHash,
            applicationUserRoleId
        );
        //some  logic create entity
        return applicationUser;
    }

}