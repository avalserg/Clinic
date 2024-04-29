using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authorization.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Application.Services;

public class CreateJwtTokenService : ICreateJwtTokenService
{
    private readonly IConfiguration _configuration;

    public CreateJwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string CreateJwtToken(ApplicationUser user, DateTime dateExpires)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.NameIdentifier, user.ApplicationUserId.ToString())
        };

        //claims.AddRange(user.ApplicationUserRole.Select(role => new Claim(ClaimTypes.Role, ((ApplicationUserRolesEnum)role.ApplicationUserRoleId).ToString())));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var credentials = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(_configuration["Jwt:Issuer"]!, _configuration["Jwt:Audience"]!, claims,
            expires: dateExpires, signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor)!;
    }
}

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    
    public string Generate(ApplicationUser user, DateTime dateExpires)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.NameIdentifier, user.ApplicationUserId.ToString())
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);
        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}