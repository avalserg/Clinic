using ManageUsers.Application.Abstractions.Messaging;
using MediatR;

namespace ManageUsers.Application.Handlers.Patient.Commands.DeletePatient;

//[RequestAuthorize]
public class DeletePatientCommand : ICommand
{
    public Guid Id { get; init; } = default!;
}