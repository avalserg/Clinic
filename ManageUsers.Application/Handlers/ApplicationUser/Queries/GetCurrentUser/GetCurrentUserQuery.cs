using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.Attributes;
using ManageUsers.Application.DTOs.ApplicationUser;
using ManageUsers.Application.DTOs.CurrentUser;
using MediatR;

namespace ManageUsers.Application.Handlers.ApplicationUser.Queries.GetCurrentUser;

[RequestAuthorize]
public record GetCurrentUserQuery :IQuery<GetCurrentUserDto> ;