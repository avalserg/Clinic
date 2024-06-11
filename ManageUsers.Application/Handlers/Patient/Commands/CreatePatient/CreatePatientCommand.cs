using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.DTOs.ApplicationUser;

namespace ManageUsers.Application.Handlers.Patient.Commands.CreatePatient;


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
