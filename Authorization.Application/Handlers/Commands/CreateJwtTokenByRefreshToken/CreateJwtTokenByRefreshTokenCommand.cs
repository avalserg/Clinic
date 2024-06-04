using Authorization.Application.DTOs;
using MediatR;

namespace Authorization.Application.Handlers.Commands.CreateJwtTokenByRefreshToken;

public class CreateJwtTokenByRefreshTokenCommand : IRequest<JwtTokenDto>
{
    public string RefreshToken { get; init; } = default!;
    public Guid ApplicationUserId { get; init; } = default!;
}