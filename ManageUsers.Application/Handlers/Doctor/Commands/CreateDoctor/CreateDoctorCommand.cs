using ManageUsers.Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageUsers.Application.Handlers.Doctor.Commands.CreateDoctor;

public class CreateDoctorCommand : IRequest<CreateApplicationUserDto>
{
    public string Login { get; init; } = default!;

    public string Password { get; init; } = default!;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTime DateBirthday { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string? Photo { get; set; }
    public int Experience { get; set; }
    public int? CabinetNumber { get; set; }
    public string Category { get; set; } = string.Empty;
   
}