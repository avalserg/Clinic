namespace Authorization.Domain;

public class ApplicationUser
{
    public Guid ApplicationUserId { get; set; }

    public string Login { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;

    public int ApplicationUserRoleId { get; set; }
    public ApplicationUserRole ApplicationUserRole { get; set; }= default!;
    public List<RefreshToken> RefreshTokens { get; set; } = default!;

    //public DateTime? UpdatedDate { get; set; }

    //public DateTime? LastSingInDate { get; set; }

    
}