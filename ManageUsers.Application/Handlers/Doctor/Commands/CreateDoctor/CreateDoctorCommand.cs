using ManageUsers.Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using ManageUsers.Application.DTOs.ApplicationUser;
using ManageUsers.Domain.Shared;

namespace ManageUsers.Application.Handlers.Doctor.Commands.CreateDoctor;

public class CreateDoctorCommand : IRequest<Result<CreateApplicationUserDto>>
{
    public string Login { get; init; } = default!;

    public string Password { get; init; } = default!;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTime DateBirthday { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string? Photo { get; set; }
    public int Experience { get; set; }
    public string CabinetNumber { get; set; }
    public string Category { get; set; } = string.Empty;
   
}