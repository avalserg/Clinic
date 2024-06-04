namespace Authorization.Domain;

public class RefreshToken
{
    public Guid RefreshTokenId { get; set; }
    
    public Guid ApplicationUserId { get; set; }
    
    public DateTime Expired { get; set; }
}