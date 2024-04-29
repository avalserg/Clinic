//using ManageUsers.Application.Handlers.Doctor.Commands.CreateDoctor;

using ManageUsers.Application.Handlers.Doctor.Commands.CreateDoctor;
using ManageUsers.Application.Handlers.Doctor.Queries.GetCountDoctors;
using ManageUsers.Application.Handlers.Doctor.Queries.GetDoctor;
using ManageUsers.Application.Handlers.Doctor.Queries.GetDoctors;
using ManageUsers.Application.Handlers.Patient.Queries.GetCountPatients;
using ManageUsers.Application.Handlers.Patient.Queries.GetPatients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageUsers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorsController:ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddDoctorAsync(
            CreateDoctorCommand createUserCommand,
            CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(createUserCommand, cancellationToken);

            return Created($"users/{user.ApplicationUserId}", user);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllDoctorsAsync(
            [FromQuery] GetDoctorsQuery getListPatientsQuery,
            CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(getListPatientsQuery, cancellationToken);
            var countPatients = await _mediator.Send(
                new GetCountDoctorsQuery() { FreeText = getListPatientsQuery.FreeText },
                cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", countPatients.ToString());
            return Ok(users);
        }
        [AllowAnonymous]
        [HttpGet("totalCount")]
        public async Task<IActionResult> GetCountDoctorsAsync(string? labelFreeText, CancellationToken cancellationToken)
        {

            var users = await _mediator.Send(new GetCountDoctorsQuery(){ FreeText = labelFreeText },
                cancellationToken);

            return Ok(users);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetDoctorQuery() { Id = id }, cancellationToken);

            return Ok(user);
        }
    }
}
