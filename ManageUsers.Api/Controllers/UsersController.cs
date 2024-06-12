using ManageUsers.Api.Abstractions;
using ManageUsers.Application.Handlers.ApplicationUser.Queries.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageUsers.Api.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        public UsersController(ISender sender) : base(sender) { }

        /// <summary>
        /// Get info about current login ApplicationUser 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("Users/CurrentUser")]
        public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
        {
            var curentUser = await Sender.Send(new GetCurrentUserQuery(), cancellationToken);
            if (curentUser.IsFailure)
            {
                return HandleFailure(curentUser);
            }
            return Ok(curentUser.Value);

        }
    }
}
