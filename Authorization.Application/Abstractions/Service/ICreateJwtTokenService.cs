using Authorization.Application.Models;

namespace Authorization.Application.Abstractions.Service;

public interface ICreateJwtTokenService
{
    string CreateJwtToken(GetApplicationUserDto user, DateTime dateExpires);
}