using Authorization.Domain;

namespace Authorization.Application.Services;

public interface IJwtProvider
{
    string Generate(ApplicationUser user, DateTime dateExpires);
}
