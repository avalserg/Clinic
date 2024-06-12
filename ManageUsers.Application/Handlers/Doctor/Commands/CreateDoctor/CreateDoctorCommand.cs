using ManageUsers.Application.DTOs.ApplicationUser;
using ManageUsers.Domain.Shared;
using MediatR;

namespace ManageUsers.Application.Handlers.Doctor.Commands.CreateDoctor;

public class CreateDoctorCommand : IRequest<Result<CreateApplicationUserDto>>
{
    public string Login { get; init; } = default!;

    public string Password { get; init; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Patronymic { get; set; } = default!;
    public DateTime DateBirthday { get; set; }
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? Photo { get; set; }
    public int Experience { get; set; }
    public string CabinetNumber { get; set; } = default!;
    public string Speciality { get; set; } = default!;
    public string Category { get; set; } = string.Empty;

}