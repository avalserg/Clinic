using Authorization.Domain.Shared;
using MediatR;

namespace Authorization.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}