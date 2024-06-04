using ManageUsers.Application.DTOs.ApplicationUser;
using MediatR;

namespace ManageUsers.Application.Handlers.ApplicationUser.Queries.GetApplicationUserById
{
    public class GetApplicationUserByIdQuery : IRequest<GetApplicationUserDto>
    {
        public Guid ApplicationUserId { get; init; } = default!;
       
    }
}
