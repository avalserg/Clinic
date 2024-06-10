using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.DTOs.Doctor;

namespace ManageUsers.Application.Handlers.Doctor.Commands.UpdateDoctor;


public sealed record UpdateDoctorCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Patronymic,
    DateTime DateBirthday,
    string Address,
    string PhoneNumber,
    int Experience,
    string CabinetNumber,
    string Category,
    string Speciality) : ICommand<GetDoctorDto>;
