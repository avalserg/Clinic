using ManageUsers.Application.Handlers.Doctor.Commands.CreateDoctor;
using ManageUsers.Application.Handlers.Patient.Commands.CreatePatient;
using ManageUsers.Application.Handlers.Patient.Commands.DeletePatient;
using ManageUsers.Application.Handlers.Patient.Queries.GetCountPatients;
using ManageUsers.Application.Handlers.Patient.Queries.GetPatient;
using ManageUsers.Application.Handlers.Patient.Queries.GetPatients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageUsers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddPatientAsync(
            CreatePatientCommand createUserCommand,
            CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(createUserCommand, cancellationToken);

            return Created($"users/{user.ApplicationUserId}", user);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetPatientQuery(){Id =id}, cancellationToken);

            return Ok(user);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync(
            [FromQuery] GetPatientsQuery getListPatientsQuery,
            CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(getListPatientsQuery, cancellationToken);
            var countPatients = await _mediator.Send(
                new GetCountPatientsQuery() { FreeText = getListPatientsQuery.FreeText },
                cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", countPatients.ToString());
            return Ok(users);
        }
        [AllowAnonymous]
        [HttpGet("totalCount")]
        public async Task<IActionResult> GetCountUserAsync(string? labelFreeText, CancellationToken cancellationToken)
        {

            var users = await _mediator.Send(new GetCountPatientsQuery() { FreeText = labelFreeText },
                cancellationToken);

            return Ok(users);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveUserAsync([FromBody] string id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeletePatientCommand() { Id = id }, cancellationToken);

            return Ok($"User with ID = {id} was deleted");
        }
    }
}
