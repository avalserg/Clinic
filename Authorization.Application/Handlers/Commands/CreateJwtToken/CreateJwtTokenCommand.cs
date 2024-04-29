using Authorization.Application.DTOs;
using MediatR;

namespace Authorization.Application.Handlers.Commands.CreateJwtToken;

//public class CreateJwtTokenCommand : IRequest<JwtTokenDto>
//{
//    public string Login { get; init; } = default!;

//    public string Password { get; init; } = default!;
//}
public  record  CreateJwtTokenCommand(string Login, string Password):IRequest<JwtTokenDto>;