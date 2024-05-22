using ManageUsers.Application.DTOs.Admin;
using ManageUsers.Application.DTOs.Doctor;
using MediatR;

namespace ManageUsers.Application.Handlers.Admin.Queries.GetAdmin
{
    public class GetAdminQuery : IRequest<GetAdminDto>
    {
        public Guid Id { get; init; } = default!;
    }
}
