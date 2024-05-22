using MediatR;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}