using ManageUsers.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace ManageUsers.Domain;

public class ApplicationUser
{

    private ApplicationUser(
        Guid applicationUserId,
        string login,
        string passwordHash,
        ApplicationUserRolesEnum applicationUserRole)
    {
        ApplicationUserId = applicationUserId;
        Login = login;
        PasswordHash = passwordHash;
        ApplicationUserRole = applicationUserRole;
    }

    private ApplicationUser() { }
    [Key]
    public Guid ApplicationUserId { get; private set; }

    public string Login { get; private set; }

    public string PasswordHash { get; private set; }
    public ApplicationUserRolesEnum ApplicationUserRole { get; private set; }

    //public DateTime CreatedDate { get; set; }

    //public DateTime? UpdatedDate { get; set; }

    //public DateTime? LastSingInDate { get; set; }

    public static ApplicationUser Create(
        Guid applicationUserId,
        string login,
        string passwordHash,
        ApplicationUserRolesEnum applicationUserRole
    )
    {
        var applicationUser = new ApplicationUser(
            applicationUserId,
            login,
            passwordHash,
            applicationUserRole
        );
        //some  logic create entity
        return applicationUser;
    }

}