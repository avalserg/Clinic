using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.DTOs;
using ManageUsers.Application.DTOs.ApplicationUser;
using ManageUsers.Domain.Shared;
using ManageUsers.Domain.ValueObjects;
using MediatR;

namespace ManageUsers.Application.Handlers.Patient.Commands.CreatePatient;

//public class CreatePatientCommand : IRequest<CreateApplicationUserDto>
//{
//    public string Login { get; init; } = default!;

//    public string Password { get; init; } = default!;
//    public string FirstNameDomainErrors { get; set; }
//    public string LastNameDomainErrors { get; set; }
//    public string PatronymicDomainErrors { get; set; }
//    public DateTime DateBirthday { get; set; }
//    public string Address { get; set; }
//    public string Phone { get; set; }
//    public string? Avatar { get; set; }
//}
public sealed record CreatePatientCommand(
   
    string Address,
    DateTime DateBirthday,
    string FirstName,
    string LastName,
    string Login,
    string Password,
 
    
    string Patronymic,
    
    string PhoneNumber,
    string PassportNumber,
    string? Avatar) : ICommand<CreateApplicationUserDto>;
