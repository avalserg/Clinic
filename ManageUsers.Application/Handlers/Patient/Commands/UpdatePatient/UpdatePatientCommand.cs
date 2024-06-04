using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.DTOs.Patient;

namespace ManageUsers.Application.Handlers.Patient.Commands.UpdatePatient;


public sealed record UpdatePatientCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Patronymic,
    DateTime DateBirthday,
    string Address,
    string PhoneNumber,
    string PassportNumber,
    string Avatar) : ICommand<GetPatientDto>;
