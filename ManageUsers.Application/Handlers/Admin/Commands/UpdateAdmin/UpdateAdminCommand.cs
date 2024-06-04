using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.DTOs.Admin;

namespace ManageUsers.Application.Handlers.Admin.Commands.UpdateAdmin;


public sealed record UpdateAdminCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Patronymic
   ) : ICommand<GetAdminDto>;
