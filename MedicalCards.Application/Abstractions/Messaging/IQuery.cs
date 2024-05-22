using MediatR;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}