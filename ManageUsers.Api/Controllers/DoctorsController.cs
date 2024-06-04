using ManageUsers.Api.Abstractions;
using ManageUsers.Api.Contracts.Doctor;
using ManageUsers.Application.Handlers.Doctor.Commands.CreateDoctor;
using ManageUsers.Application.Handlers.Doctor.Commands.UpdateDoctor;
using ManageUsers.Application.Handlers.Doctor.Queries.GetCountDoctors;
using ManageUsers.Application.Handlers.Doctor.Queries.GetDoctor;
using ManageUsers.Application.Handlers.Doctor.Queries.GetDoctors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ManageUsers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorsController : ApiController
    {
        

        public DoctorsController(ISender sender) : base(sender)
        {

        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDoctorAsync(
            CreateDoctorCommand createUserCommand,
            CancellationToken cancellationToken)
        {
            var user = await Sender.Send(createUserCommand, cancellationToken);
            if (user.IsFailure)
            {
                return HandleFailure(user);
            }
            //return Created($"users/{user.Value.ApplicationUserId}", user);
            return CreatedAtAction("AddDoctor",new{id = user.Value.ApplicationUserId}, user);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllDoctorsAsync(
            [FromQuery] GetDoctorsQuery getListPatientsQuery,
            CancellationToken cancellationToken)
        {
            var users = await Sender.Send(getListPatientsQuery, cancellationToken);
            var countPatients = await Sender.Send(
                new GetCountDoctorsQuery() { FreeText = getListPatientsQuery.FreeText },
                cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", countPatients.ToString());
            return Ok(users);
        }
        [AllowAnonymous]
        [HttpGet("totalCount")]
        public async Task<IActionResult> GetCountDoctorsAsync(string? labelFreeText, CancellationToken cancellationToken)
        {

            var users = await Sender.Send(new GetCountDoctorsQuery(){ FreeText = labelFreeText },
                cancellationToken);

            return Ok(users);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var user = await Sender.Send(new GetDoctorQuery() { Id = id }, cancellationToken);

            return Ok(user);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDoctorRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateDoctorAsync(
            [FromRoute] Guid id,
            [FromBody] UpdateDoctorRequest updateDoctorRequest,
            CancellationToken cancellationToken)
        {
            // CultureInfo provider = CultureInfo.CurrentCulture;
            var command = new UpdateDoctorCommand(
                id,
                updateDoctorRequest.FirstName,
                updateDoctorRequest.LastName,
                updateDoctorRequest.Patronymic,
                DateTime.Parse(updateDoctorRequest.DateBirthday, null, DateTimeStyles.RoundtripKind),
                updateDoctorRequest.Address,
                updateDoctorRequest.PhoneNumber.Value,
                updateDoctorRequest.Experience,
                updateDoctorRequest.CabinetNumber,
                updateDoctorRequest.Category
            );
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.Value);
        }
    }
}
