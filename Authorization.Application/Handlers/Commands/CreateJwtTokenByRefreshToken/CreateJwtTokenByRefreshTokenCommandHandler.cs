using Authorization.Application.Abstractions.ExternalProviders;
using Authorization.Application.Abstractions.Persistence.Repository.Read;
using Authorization.Application.Abstractions.Persistence.Repository.Writing;
using Authorization.Application.DTOs;
using Authorization.Application.Exceptions;
using Authorization.Application.Services;
using Authorization.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Authorization.Application.Handlers.Commands.CreateJwtTokenByRefreshToken;

internal class CreateJwtTokenByRefreshTokenCommandHandler : IRequestHandler<CreateJwtTokenByRefreshTokenCommand, JwtTokenDto>
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
    
    public async Task<JwtTokenDto> Handle(CreateJwtTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenGuid = Guid.Parse(request.RefreshToken);
        var refreshTokenFormDb = await _refreshTokens.AsAsyncRead().SingleOrDefaultAsync(e => e.RefreshTokenId == refreshTokenGuid&&e.ApplicationUserId==request.ApplicationUserId, cancellationToken);
        if (refreshTokenFormDb is null)
        {
            throw new ForbiddenException();
        }
        if (refreshTokenFormDb.Expired < DateTime.UtcNow)
        {
            throw new ForbiddenException();
        }
        // var applicationUser = await _applicationUsersProviders.GetApplicationUserAsync(request.Login, request.Password, cancellationToken);
        var user = await _applicationUsersProviders.GetApplicationUserByIdAsync(request.ApplicationUserId,cancellationToken);

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