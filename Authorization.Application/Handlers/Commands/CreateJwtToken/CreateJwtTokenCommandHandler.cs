using Authorization.Application.Abstractions.ExternalProviders;
using Authorization.Application.Abstractions.Persistence.Repository.Read;
using Authorization.Application.Abstractions.Persistence.Repository.Writing;
using Authorization.Application.Abstractions.Service;
using Authorization.Application.DTOs;
using Authorization.Application.Exceptions;
using Authorization.Application.Services;
using Authorization.Application.Utils;
using Authorization.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Authorization.Application.Handlers.Commands.CreateJwtToken;

internal class CreateJwtTokenCommandHandler : IRequestHandler<CreateJwtTokenCommand, JwtTokenDto>
{
    private readonly IBaseReadRepository<ApplicationUser> _users;
    private readonly IBaseWriteRepository<RefreshToken> _refreshTokens;
    private readonly ICreateJwtTokenService _createJwtTokenService;
    private readonly IJwtProvider _jwtProvider;
    private readonly IConfiguration _configuration;
    private readonly IApplicationUsersProviders _applicationUsersProviders;



    public CreateJwtTokenCommandHandler(
        IBaseReadRepository<ApplicationUser> users, 
        IBaseWriteRepository<RefreshToken> refreshTokens,
        ICreateJwtTokenService createJwtTokenService,
        IConfiguration configuration, IJwtProvider jwtProvider, 
        ICurrentUserService currentUserService,
        IApplicationUsersProviders applicationUsersProviders)
    {
        _users = users;
        _refreshTokens = refreshTokens;
        _createJwtTokenService = createJwtTokenService;
        _configuration = configuration;
        _jwtProvider = jwtProvider;
        _applicationUsersProviders = applicationUsersProviders;
    }
    
    public async Task<JwtTokenDto> Handle(CreateJwtTokenCommand request, CancellationToken cancellationToken)
    {

        var user = await _users.AsAsyncRead().SingleOrDefaultAsync(u => u.Login == request.Login.Trim(), cancellationToken);
        var applicationUser = await _applicationUsersProviders.GetApplicationUserAsync(user.ApplicationUserId, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException($"User with login {request.Login} don't exist");
        }

        if (!PasswordHashUtil.Verify(request.Password, user.PasswordHash))
        {
            throw new ForbiddenException();
        }

        var jwtTokenDateExpires = DateTime.UtcNow.AddSeconds(int.Parse(_configuration["TokensLifeTime:JwtToken"]!));
        var refreshTokenDateExpires = DateTime.UtcNow.AddSeconds(int.Parse(_configuration["TokensLifeTime:RefreshToken"]!));
        var token = _jwtProvider.Generate(user, jwtTokenDateExpires);
        //var token = _createJwtTokenService.CreateJwtToken(user, jwtTokenDateExpires);
        var refreshToken = await _refreshTokens.AddAsync(new RefreshToken {RefreshTokenId = Guid.NewGuid(), ApplicationUserId = user.ApplicationUserId, Expired = refreshTokenDateExpires}, cancellationToken);
        GetUserDto application = new GetUserDto()
        {
            Login = user.Login,
            ApplicationUserId = user.ApplicationUserId,
            ApplicationUserRole = user.ApplicationUserRole.Name.ToUpper(),

        };
        return new JwtTokenDto
        {
            JwtToken = token,
            RefreshToken = refreshToken.RefreshTokenId.ToString(),
            Expires = jwtTokenDateExpires,
            ApplicationUser = application
        };
    }
}