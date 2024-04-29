using Authorization.Domain;

namespace Authorization.Application.Services;

public interface ICreateJwtTokenService
{
    string CreateJwtToken(ApplicationUser user, DateTime dateExpires);
}