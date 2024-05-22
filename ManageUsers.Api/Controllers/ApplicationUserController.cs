using ManageUsers.Api.Abstractions;
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
        /// Get certain admin by id
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
            var user = await Sender.Send(new GetApplicationUserQuery() { ApplicationUserId = id }, cancellationToken);

            return Ok(user);
        }
    }
}
