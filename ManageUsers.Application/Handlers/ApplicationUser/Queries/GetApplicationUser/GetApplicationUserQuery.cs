using ManageUsers.Application.DTOs.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageUsers.Application.DTOs.ApplicationUser;

namespace ManageUsers.Application.Handlers.ApplicationUser.Queries.GetApplicationUser
{
    public class GetApplicationUserQuery : IRequest<GetApplicationUserDto>
    {
        public string Login { get; init; } = default!;
        public string Password { get; init; } = default!;
    }
}
