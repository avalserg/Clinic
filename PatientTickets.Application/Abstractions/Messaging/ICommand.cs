using MediatR;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
