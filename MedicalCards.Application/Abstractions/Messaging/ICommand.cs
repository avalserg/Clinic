using MediatR;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
