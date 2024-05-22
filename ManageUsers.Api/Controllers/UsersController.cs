using ManageUsers.Application.Handlers.ApplicationUser.Queries.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageUsers.Api.Controllers
{
    [Authorize]
    public class UsersController :ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get info about current login ApplicationUser 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("Users/CurrentUser")]
        public async Task<IActionResult> GetCurrentUser( CancellationToken cancellationToken)
        {
            var curentUser = await _mediator.Send(new GetCurrentUserQuery(), cancellationToken);
            return Ok(curentUser);

        }
    }
}
