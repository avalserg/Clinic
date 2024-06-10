using ManageUsers.Application.Abstractions.Messaging;

namespace ManageUsers.Application.Handlers.Doctor.Commands.DeleteDoctor;

public class DeleteDoctorCommand : ICommand
{
    public Guid Id { get; init; } = default!;

}