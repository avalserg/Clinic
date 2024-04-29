using ManageUsers.Application.DTOs;
using MediatR;

namespace ManageUsers.Application.Handlers.Patient.Commands.CreatePatient;

public class CreatePatientCommand : IRequest<CreateApplicationUserDto>
{
    public string Login { get; init; } = default!;

    public string Password { get; init; } = default!;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTime DateBirthday { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string? Avatar { get; set; }
}