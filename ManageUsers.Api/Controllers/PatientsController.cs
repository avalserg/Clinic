using ManageUsers.Api.Abstractions;
using ManageUsers.Api.Contracts.Patient;
using ManageUsers.Application.Handlers.Patient.Commands.CreatePatient;
using ManageUsers.Application.Handlers.Patient.Commands.DeletePatient;
using ManageUsers.Application.Handlers.Patient.Commands.UpdatePatient;
using ManageUsers.Application.Handlers.Patient.Queries.GetCountPatients;
using ManageUsers.Application.Handlers.Patient.Queries.GetCountPatientsByAge;
using ManageUsers.Application.Handlers.Patient.Queries.GetPatient;
using ManageUsers.Application.Handlers.Patient.Queries.GetPatients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ManageUsers.Api.Controllers
{

    [Route("[controller]")]
    public class PatientsController : ApiController
    {

        public PatientsController(ISender sender) : base(sender)
        {

        }

        /// <summary>
        /// Add patient
        /// </summary>
        /// <param name="createPatientRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddPatientAsync(
            CreatePatientRequest createPatientRequest,
            CancellationToken cancellationToken)
        {
            var command = new CreatePatientCommand(
                createPatientRequest.Address,
                DateTime.Parse(createPatientRequest.DateBirthday, null, DateTimeStyles.RoundtripKind),
                createPatientRequest.FirstName,
                createPatientRequest.LastName,
                createPatientRequest.Login,
                createPatientRequest.Password,
                createPatientRequest.Patronymic,
                createPatientRequest.PhoneNumber,
                createPatientRequest.PassportNumber,
                createPatientRequest.Avatar

            );
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Created($"users/{result.Value.ApplicationUserId}", result.Value);
        }

        /// <summary>
        /// Get certain patient by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var user = await Sender.Send(new GetPatientQuery() { Id = id }, cancellationToken);

            return Ok(user);
        }

        /// <summary>
        /// Get all patients with search, sorting and limit
        /// </summary>
        /// <param name="getListPatientsQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllPatientsAsync(
            [FromQuery] GetPatientsQuery getListPatientsQuery,
            CancellationToken cancellationToken)
        {
            var users = await Sender.Send(getListPatientsQuery, cancellationToken);
            var countPatients = await Sender.Send(
                new GetCountPatientsQuery() { FreeText = getListPatientsQuery.FreeText },
                cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", countPatients.ToString());
            return Ok(users);
        }

        /// <summary>
        /// Get count all patients
        /// </summary>
        /// <param name="labelFreeText"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("totalCount")]
        public async Task<IActionResult> GetCountPatientAsync(string? labelFreeText, CancellationToken cancellationToken)
        {

            var users = await Sender.Send(new GetCountPatientsQuery() { FreeText = labelFreeText },
                cancellationToken);

            return Ok(users);
        }
        /// <summary>
        /// Count all doctors
        /// </summary>
        /// <param name="labelFreeText"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("countByAge")]
        public async Task<IActionResult> GetCountPatientsByAgeAsync(CancellationToken cancellationToken)
        {

            var count = await Sender.Send(new GetCountPatientsByAgeQuery(), cancellationToken);
            return Ok(count);
        }
        /// <summary>
        /// Update patient
        /// </summary>
        /// <param name="id">Patient id</param>
        /// <param name="updatePatientRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdatePatientAsync(
            [FromRoute] Guid id,
            [FromBody] UpdatePatientRequest updatePatientRequest,
            CancellationToken cancellationToken)
        {
            // CultureInfo provider = CultureInfo.CurrentCulture;
            var command = new UpdatePatientCommand(
                id,
                updatePatientRequest.FirstName,
                updatePatientRequest.LastName,
                updatePatientRequest.Patronymic,
                DateTime.Parse(updatePatientRequest.DateBirthday, null, DateTimeStyles.RoundtripKind),
                updatePatientRequest.Address,
                updatePatientRequest.PhoneNumber,
                updatePatientRequest.PassportNumber,
                updatePatientRequest.Avatar
            );
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Delete patient
        /// </summary>

        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePatientAsync([FromBody] DeletePatientRequest request, CancellationToken cancellationToken)
        {
            await Sender.Send(new DeletePatientCommand() { Id = Guid.Parse(request.Id) }, cancellationToken);

            return Ok($"User with ID = {request.Id} was deleted");
        }


    }
}
