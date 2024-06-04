using Authorization.Application.Models;
using Authorization.Domain;

namespace Authorization.Application.Services;

public interface ICreateJwtTokenService
{
    string CreateJwtToken(GetApplicationUserDto user, DateTime dateExpires);
}