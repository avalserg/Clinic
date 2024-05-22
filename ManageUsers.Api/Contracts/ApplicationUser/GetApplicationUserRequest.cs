using System.ComponentModel.DataAnnotations;

namespace ManageUsers.Api.Contracts.ApplicationUser
{
    public record GetApplicationUserRequest(
        [Required] string Login,
        [Required] string Password
    );

}
