using Authorization.Application.Models;

namespace Authorization.Application.Abstractions.Service;

public interface IJwtProvider
{
    string Generate(GetApplicationUserDto user, DateTime dateExpires);
}
