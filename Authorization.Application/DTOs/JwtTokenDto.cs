using Authorization.Application.Models;

namespace Authorization.Application.DTOs;

public class JwtTokenDto
{
    public string JwtToken { get; set; } = default!;

    public string RefreshToken { get; set; } = default!;

    public DateTime Expires { get; set; }
    public GetApplicationUserDto ApplicationUser { get; set; }

}