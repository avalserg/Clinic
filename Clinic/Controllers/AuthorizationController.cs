using Authorization.Application.Handlers.Commands.CreateJwtToken;
using Authorization.Application.Handlers.Commands.CreateJwtTokenByRefreshToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Api.Controllers
{
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator _mediator;


        public AuthorizationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createJwtTokenCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("CreateJwtToken")]
        public async Task<IActionResult> CreateJwtTokenAsync([FromBody] CreateJwtTokenCommand createJwtTokenCommand, CancellationToken cancellationToken)
        {
            var createdToken = await _mediator.Send(createJwtTokenCommand, cancellationToken);
            return Ok(createdToken);

        }
        [AllowAnonymous]
        [HttpPost("CreateJwtTokenByRefreshToken")]
        public async Task<IActionResult> CreateJwtTokenByRefreshTokenAsync([FromBody] CreateJwtTokenByRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var createdToken = await _mediator.Send(command, cancellationToken);
            return Ok(createdToken);

        }
    }
}
