using Authorization.Api.Abstractions;
using Authorization.Application.Handlers.Commands.CreateJwtToken;
using Authorization.Application.Handlers.Commands.CreateJwtTokenByRefreshToken;
using Authorization.Application.Handlers.Commands.DeleteRefreshToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ApiController
    {
        public AuthorizationController(ISender sender) : base(sender)
        {

        }
        /// <summary>
        ///  Create access and jwt token
        /// </summary>
        /// <param name="createJwtTokenCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("CreateJwtToken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateJwtTokenAsync([FromBody] CreateJwtTokenCommand createJwtTokenCommand, CancellationToken cancellationToken)
        {
            var createdToken = await Sender.Send(createJwtTokenCommand, cancellationToken);
            if (createdToken.IsFailure)
            {
                return HandleFailure(createdToken);
            }
            return Ok(createdToken.Value);

        }
        /// <summary>
        /// Create access token by refresh  token
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("CreateJwtTokenByRefreshToken")]
        public async Task<IActionResult> CreateJwtTokenByRefreshTokenAsync([FromBody] CreateJwtTokenByRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var createdToken = await Sender.Send(command, cancellationToken);
            return Ok(createdToken);

        }
        /// <summary>
        /// Delete refresh token from db for authorize users
        /// </summary>
        /// <param name="applicationUserId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteRefreshTokenAsync(CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new DeleteRefreshTokenCommand(), cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok($"{result.IsSuccess}");
        }
    }
}
