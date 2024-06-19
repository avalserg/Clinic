using ManageUsers.Api.Abstractions;
using ManageUsers.Api.Contracts.Doctor;
using ManageUsers.Application.Handlers.Doctor.Commands.CreateDoctor;
using ManageUsers.Application.Handlers.Doctor.Commands.DeleteDoctor;
using ManageUsers.Application.Handlers.Doctor.Commands.UpdateDoctor;
using ManageUsers.Application.Handlers.Doctor.Queries.GetCountDoctors;
using ManageUsers.Application.Handlers.Doctor.Queries.GetCountDoctorsByCategories;
using ManageUsers.Application.Handlers.Doctor.Queries.GetDoctor;
using ManageUsers.Application.Handlers.Doctor.Queries.GetDoctors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ManageUsers.Api.Controllers
{

    [Route("[controller]")]
    public class DoctorsController : ApiController
    {
        public DoctorsController(ISender sender) : base(sender)
        {
        }
        /// <summary>
        /// Add doctor
        /// </summary>
        /// <param name="createDoctorCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDoctorAsync(
            CreateDoctorCommand createDoctorCommand,
            CancellationToken cancellationToken)
        {
            var user = await Sender.Send(createDoctorCommand, cancellationToken);
            if (user.IsFailure)
            {
                return HandleFailure(user);
            }
            return CreatedAtAction("AddDoctor", new { id = user.Value.ApplicationUserId }, user);
        }
        /// <summary>
        /// Get doctors list
        /// </summary>
        /// <param name="getListPatientsQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Count all doctors
        /// </summary>
        /// <param name="labelFreeText"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("totalCount")]
        public async Task<IActionResult> GetCountDoctorsAsync(string? labelFreeText, CancellationToken cancellationToken)
        {

            var usersCount = await Sender.Send(new GetCountDoctorsQuery() { FreeText = labelFreeText },
                cancellationToken);
            var count = await Sender.Send(new GetCountDoctorsByCategoriesQuery(), cancellationToken);
            return Ok(usersCount);
        }
        /// <summary>
        /// Count all doctors
        /// </summary>
        /// <param name="labelFreeText"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("countByCategories")]
        public async Task<IActionResult> GetCountDoctorsByCategoriesAsync(CancellationToken cancellationToken)
        {

            var count = await Sender.Send(new GetCountDoctorsByCategoriesQuery(), cancellationToken);
            return Ok(count);
        }
        /// <summary>
        /// Get certain doctor by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
        ///  Update doctor 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDoctorRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateDoctorAsync(
            [FromRoute] DeleteDoctorRequest request,
            [FromBody] UpdateDoctorRequest updateDoctorRequest,
            CancellationToken cancellationToken)
        {
            // CultureInfo provider = CultureInfo.CurrentCulture;
            var command = new UpdateDoctorCommand(
                Guid.Parse(request.Id),
                updateDoctorRequest.FirstName,
                updateDoctorRequest.LastName,
                updateDoctorRequest.Patronymic,
                DateTime.Parse(updateDoctorRequest.DateBirthday, null, DateTimeStyles.RoundtripKind),
                updateDoctorRequest.Address,
                updateDoctorRequest.PhoneNumber,
                updateDoctorRequest.Experience,
                updateDoctorRequest.CabinetNumber,
                updateDoctorRequest.Category,
                updateDoctorRequest.Speciality
            );
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.Value);
        }
        /// <summary>
        /// Delete doctor
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteDoctorAsync([FromBody] DeleteDoctorRequest request, CancellationToken cancellationToken)
        {
            await Sender.Send(new DeleteDoctorCommand() { Id = Guid.Parse(request.Id) }, cancellationToken);

            return Ok($"User with ID = {request.Id} was deleted");
        }

    }
}
