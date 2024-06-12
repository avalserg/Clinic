using Authorization.Domain.Shared;

namespace Authorization.Domain.Errors;

public static class DomainErrors
{

    public static class RefreshTokenDomainErrors
    {
        public static readonly Func<string, Error> UserNotFound = id => new Error(
            $"RefreshTokenDomainErrors.UserNotFound",
            $"The User with the identifier {id} was not found.");
        public static readonly Func<string, Error> RefreshTokenNotFound = refreshToken => new Error(
            $"RefreshTokenDomainErrors.RefreshTokenNotFound",
            $"The RefreshToken {refreshToken} was not found.");
        public static readonly Func<Guid?, Error> RefreshTokenWithOwnerNotFound = id => new Error(
            $"RefreshTokenDomainErrors.RefreshTokenWithOwnerNotFound",
            $"The RefreshToken with owner id = {id} was not found.");
        public static readonly Func<Guid, Error> CurrentUserIdNotEqualRequestId = id => new Error(
            $"RefreshTokenDomainErrors.CurrentUserIdNotEqualRequestId",
            $"The current user not equal user with request id = {id}");
        public static readonly Error CurrentUserIdNotFound = new(
            $"RefreshTokenDomainErrors.CurrentUserIdNotEqualRequestId",
            $"The current user not found");
        public static readonly Func<string, Error> RefreshTokenExpired = refreshToken => new Error(
              $"RefreshTokenDomainErrors.RefreshTokenExpired",
              $"The RefreshToken {refreshToken} was expired.");

    }

}
