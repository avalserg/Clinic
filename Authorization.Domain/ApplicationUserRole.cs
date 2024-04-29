namespace Authorization.Domain;

public class ApplicationUserRole
{
    public int ApplicationUserRoleId { get; set; }

    public string Name { get; set; } = default!;

    public List<ApplicationUser> ApplicationUsers { get; set; } = default!;
}