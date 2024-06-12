using Authorization.Application.Models;
using Authorization.Domain;

namespace Authorization.Application.Services;

public interface IJwtProvider
{
    string Generate(GetApplicationUserDto user, DateTime dateExpires);
}
