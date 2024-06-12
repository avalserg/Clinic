using Authorization.Domain.Shared;
using MediatR;

namespace Authorization.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
