using ManageUsers.Domain.Shared;
using MediatR;

namespace ManageUsers.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}