using ManageUsers.Application.Abstractions.Messaging;

namespace ManageUsers.Application.Handlers.Patient.Commands.DeletePatient;


public class DeletePatientCommand : ICommand
{
    public Guid Id { get; init; } = default!;
}