using Authorization.Application.Abstractions.Messaging;
using Authorization.Application.DTOs;

namespace Authorization.Application.Handlers.Commands.CreateJwtToken;

public record CreateJwtTokenCommand(string Login, string Password) : ICommand<JwtTokenDto>;