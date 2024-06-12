using ManageUsers.Application.DTOs.Admin;
using MediatR;

namespace ManageUsers.Application.Handlers.Admin.Queries.GetAdmin
{
    public class GetAdminQuery : IRequest<GetAdminDto>
    {
        public Guid Id { get; init; } = default!;
    }
}
