namespace Authorization.Domain;

public class RefreshToken
{
    public Guid RefreshTokenId { get; set; }
    
    public int ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = default!;
    
    public DateTime Expired { get; set; }
}