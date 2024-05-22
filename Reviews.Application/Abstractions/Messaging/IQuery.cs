using MediatR;
using Reviews.Domain.Shared;

namespace Reviews.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}