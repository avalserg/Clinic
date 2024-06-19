using Authorization.Application.Abstractions.ExternalProviders;
using Authorization.Application.Abstractions.Messaging;
using Authorization.Application.Abstractions.Persistence.Repository.Writing;
using Authorization.Application.Abstractions.Service;
using Authorization.Application.DTOs;
using Authorization.Application.Models;
using Authorization.Domain;
using Authorization.Domain.Errors;
using Authorization.Domain.Shared;
using Microsoft.Extensions.Configuration;

namespace Authorization.Application.Handlers.Commands.CreateJwtToken;

internal class CreateJwtTokenCommandHandler : ICommandHandler<CreateJwtTokenCommand, JwtTokenDto>
{

    private readonly IBaseWriteRepository<RefreshToken> _refreshTokens;
    private readonly ICreateJwtTokenService _createJwtTokenService;
    private readonly IJwtProvider _jwtProvider;
    private readonly IConfiguration _configuration;
    private readonly IApplicationUsersProviders _applicationUsersProviders;



    public CreateJwtTokenCommandHandler(

        IBaseWriteRepository<RefreshToken> refreshTokens,
        ICreateJwtTokenService createJwtTokenService,
        IConfiguration configuration, IJwtProvider jwtProvider,
        ICurrentUserService currentUserService,
        IApplicationUsersProviders applicationUsersProviders)
    {

        _refreshTokens = refreshTokens;
        _createJwtTokenService = createJwtTokenService;
        _configuration = configuration;
        _jwtProvider = jwtProvider;
        _applicationUsersProviders = applicationUsersProviders;
    }

    public async Task<Result<JwtTokenDto>> Handle(CreateJwtTokenCommand request, CancellationToken cancellationToken)
    {
        var applicationUser = await _applicationUsersProviders.GetApplicationUserAsync(request.Login, request.Password, cancellationToken);
        if (applicationUser is null)
        {
            return Result.Failure<JwtTokenDto>(DomainErrors.RefreshTokenDomainErrors.UserNotFound(request.Login));
        }

        var jwtTokenDateExpires = DateTime.UtcNow.AddSeconds(int.Parse(_configuration["TokensLifeTime:JwtToken"]!));
        var refreshTokenDateExpires = DateTime.UtcNow.AddSeconds(int.Parse(_configuration["TokensLifeTime:RefreshToken"]!));
        var token = _jwtProvider.Generate(applicationUser, jwtTokenDateExpires);
        var refreshToken = await _refreshTokens.AddAsync(new RefreshToken
        {
            RefreshTokenId = Guid.NewGuid(),
            ApplicationUserId = applicationUser.ApplicationUserId,
            Expired = refreshTokenDateExpires
        }, cancellationToken);

        var application = new GetApplicationUserDto()
        {
            Login = applicationUser.Login,
            ApplicationUserId = applicationUser.ApplicationUserId,
            ApplicationUserRole = applicationUser.ApplicationUserRole,

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