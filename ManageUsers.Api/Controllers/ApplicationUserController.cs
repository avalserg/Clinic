using ManageUsers.Api.Abstractions;
using ManageUsers.Api.Contracts.ApplicationUser;
using ManageUsers.Application.Handlers.ApplicationUser.Queries.GetApplicationUser;
using ManageUsers.Application.Handlers.ApplicationUser.Queries.GetApplicationUserById;
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
            var user = await Sender.Send(new GetApplicationUserQuery() { Login = getApplicationUserRequest.Login, Password = getApplicationUserRequest.Password }, cancellationToken);

            return Ok(user);
        }

        /// <summary>
        /// Get certain user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationUserByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var user = await Sender.Send(new GetApplicationUserByIdQuery() { ApplicationUserId = id }, cancellationToken);

            return Ok(user);
        }
    }
}
