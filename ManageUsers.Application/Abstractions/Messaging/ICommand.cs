using ManageUsers.Domain.Shared;
using MediatR;

namespace ManageUsers.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
