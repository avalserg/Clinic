using Authorization.Domain;
using AutoMapper;

namespace Authorization.Application.DTOs;

public class JwtTokenDto
{
    public string JwtToken { get; set; } = default!;

    public string RefreshToken { get; set; } = default!;
    
    public DateTime Expires { get; set; }
    public GetUserDto ApplicationUser { get; set; }
   
}