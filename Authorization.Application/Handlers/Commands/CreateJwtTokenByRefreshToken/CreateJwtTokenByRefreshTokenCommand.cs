using Authorization.Application.Abstractions.Messaging;
using Authorization.Application.DTOs;

namespace Authorization.Application.Handlers.Commands.CreateJwtTokenByRefreshToken;

public class CreateJwtTokenByRefreshTokenCommand : ICommand<JwtTokenDto>
{
    public string RefreshToken { get; init; } = default!;
    public Guid ApplicationUserId { get; init; } = default!;
}