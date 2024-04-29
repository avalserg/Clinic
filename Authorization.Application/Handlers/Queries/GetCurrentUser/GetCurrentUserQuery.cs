using Authorization.Application.Attributes;
using Authorization.Application.DTOs;
using MediatR;

namespace Authorization.Application.Handlers.Queries.GetCurrentUser;

[RequestAuthorize]
public record GetCurrentUserQuery : IRequest<GetUserDto>;