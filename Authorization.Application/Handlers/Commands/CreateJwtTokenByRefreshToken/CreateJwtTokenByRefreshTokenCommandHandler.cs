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
    
    private readonly IBaseReadRepository<ApplicationUser> _users;
    
    private readonly ICreateJwtTokenService _createJwtTokenService;
    
    private readonly IConfiguration _configuration;

    public CreateJwtTokenByRefreshTokenCommandHandler(
        IBaseWriteRepository<RefreshToken> refreshTokens,
        IBaseReadRepository<ApplicationUser> users,
        ICreateJwtTokenService createJwtTokenService,
        IConfiguration configuration)
    {
        _refreshTokens = refreshTokens;
        _users = users;
        _createJwtTokenService = createJwtTokenService;
        _configuration = configuration;
    }
    
    public async Task<JwtTokenDto> Handle(CreateJwtTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenGuid = Guid.Parse(request.RefreshToken);
        var refreshTokenFormDb = await _refreshTokens.AsAsyncRead().SingleOrDefaultAsync(e => e.RefreshTokenId == refreshTokenGuid, cancellationToken);
        if (refreshTokenFormDb is null )
        {
            throw new ForbiddenException();
        }  
        if (refreshTokenFormDb.Expired < DateTime.UtcNow)
        {
            throw new ForbiddenException();
        }
        
        var user = await _users.AsAsyncRead().SingleAsync(u => u.ApplicationUserId == refreshTokenFormDb.ApplicationUserId, cancellationToken);

        var jwtTokenDateExpires = DateTime.UtcNow.AddSeconds(int.Parse(_configuration["TokensLifeTime:JwtToken"]!));
        var refreshTokenDateExpires = DateTime.UtcNow.AddSeconds(int.Parse(_configuration["TokensLifeTime:RefreshToken"]!));
        
        var jwtToken = _createJwtTokenService.CreateJwtToken(user, jwtTokenDateExpires);

        refreshTokenFormDb.Expired = refreshTokenDateExpires;
        await _refreshTokens.UpdateAsync(refreshTokenFormDb, cancellationToken);
        
        return new JwtTokenDto
        {
            JwtToken = jwtToken,
            RefreshToken = refreshTokenFormDb.RefreshTokenId.ToString(),
            Expires = jwtTokenDateExpires
        };
    }
}