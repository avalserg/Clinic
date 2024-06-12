namespace ManageUsers.Domain;

public class ApplicationUserRole
{
    private ApplicationUserRole(
        int applicationUserRoleId,
        string name)
    {
        ApplicationUserRoleId = applicationUserRoleId;
        Name = name;

    }

    private ApplicationUserRole() { }
    public int ApplicationUserRoleId { get; set; }

    public string Name { get; set; } = default!;

    public static ApplicationUserRole Create(
        int applicationUserRoleId,
        string name
    )
    {
        var applicationUserRole = new ApplicationUserRole(
            applicationUserRoleId,
            name

        );

        //some  logic to create entity
        return applicationUserRole;
    }
}