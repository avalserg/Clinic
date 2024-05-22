using ManageUsers.Api.Abstractions;
using ManageUsers.Api.Contracts.ApplicationUser;
using ManageUsers.Application.Handlers.Admin.Queries.GetAdmin;
using ManageUsers.Application.Handlers.ApplicationUser.Queries.GetApplicationUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageUsers.Api.Controllers
{
    [Route("[controller]")]
    public class ApplicationUserController : ApiController
    {
        public ApplicationUserController(ISender sender) : base(sender)
        {
        }
        /// <summary>
        /// Get certain user
        /// </summary>
        /// <param name="getApplicationUserRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetApplicationUserAsync(
            [FromBody] GetApplicationUserRequest getApplicationUserRequest,
            
            CancellationToken cancellationToken)
        {
            var user = await Sender.Send(new GetApplicationUserQuery() { Login = getApplicationUserRequest.Login, Password =getApplicationUserRequest.Password }, cancellationToken);

            return Ok(user);
        }
    }
}
