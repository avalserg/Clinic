using MediatR;

namespace ManageUsers.Application.Handlers.Patient.Commands.DeletePatient;

//[RequestAuthorize]
public class DeletePatientCommand : IRequest
{
    public string Id { get; init; } = default!;
}