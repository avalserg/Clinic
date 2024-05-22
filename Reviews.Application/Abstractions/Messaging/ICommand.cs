using MediatR;
using Reviews.Domain.Shared;

namespace Reviews.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
