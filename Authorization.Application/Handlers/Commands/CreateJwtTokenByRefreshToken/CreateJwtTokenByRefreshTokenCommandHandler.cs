using Authorization.Application.Abstractions.ExternalProviders;
using Authorization.Application.Abstractions.Messaging;
using Authorization.Application.Abstractions.Persistence.Repository.Writing;
using Authorization.Application.Abstractions.Service;
using Authorization.Application.DTOs;
using Authorization.Domain;
using Authorization.Domain.Errors;
using Authorization.Domain.Shared;
using Microsoft.Extensions.Configuration;

namespace Authorization.Application.Handlers.Commands.CreateJwtTokenByRefreshToken;

internal class CreateJwtTokenByRefreshTokenCommandHandler : ICommandHandler<CreateJwtTokenByRefreshTokenCommand, JwtTokenDto>
{
    private readonly IBaseWriteRepository<RefreshToken> _refreshTokens;
    private readonly IApplicationUsersProviders _applicationUsersProviders;


    private readonly ICreateJwtTokenService _createJwtTokenService;

    private readonly IConfiguration _configuration;

    public CreateJwtTokenByRefreshTokenCommandHandler(
        IBaseWriteRepository<RefreshToken> refreshTokens,

        ICreateJwtTokenService createJwtTokenService,
        IConfiguration configuration, IApplicationUsersProviders applicationUsersProviders)
    {
        _refreshTokens = refreshTokens;
        _createJwtTokenService = createJwtTokenService;
        _configuration = configuration;
        _applicationUsersProviders = applicationUsersProviders;
    }

    public async Task<Result<JwtTokenDto>> Handle(CreateJwtTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenGuid = Guid.Parse(request.RefreshToken);
        var refreshTokenFormDb = await _refreshTokens.AsAsyncRead().SingleOrDefaultAsync(e => e.RefreshTokenId == refreshTokenGuid && e.ApplicationUserId == request.ApplicationUserId, cancellationToken);
        if (refreshTokenFormDb is null)
        {
            return Result.Failure<JwtTokenDto>(DomainErrors.RefreshTokenDomainErrors.RefreshTokenNotFound(request.RefreshToken));
        }
        if (refreshTokenFormDb.Expired < DateTime.UtcNow)
        {
            return Result.Failure<JwtTokenDto>(DomainErrors.RefreshTokenDomainErrors.RefreshTokenExpired(refreshTokenFormDb.RefreshTokenId.ToString()));
        }

        var user = await _applicationUsersProviders.GetApplicationUserByIdAsync(request.ApplicationUserId, cancellationToken);

        var jwtTokenDateExpires = DateTime.UtcNow.AddSeconds(int.Parse(_configuration["TokensLifeTime:JwtToken"]!));
        var refreshTokenDateExpires = DateTime.UtcNow.AddSeconds(int.Parse(_configuration["TokensLifeTime:RefreshToken"]!));

        var jwtToken = _createJwtTokenService.CreateJwtToken(user, jwtTokenDateExpires);

        refreshTokenFormDb.Expired = refreshTokenDateExpires;
        await _refreshTokens.UpdateAsync(refreshTokenFormDb, cancellationToken);

        return new JwtTokenDto
        {
            JwtToken = jwtToken,
            RefreshToken = refreshTokenFormDb.RefreshTokenId.ToString(),
            Expires = jwtTokenDateExpires,
            ApplicationUser = user
        };
    }
}