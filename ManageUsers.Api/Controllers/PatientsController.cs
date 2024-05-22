using ManageUsers.Api.Abstractions;
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

    [Route("[controller]")]
    public class PatientsController : ApiController
    {
      
        public PatientsController(ISender sender):base(sender)
        {
            
        }
        /// <summary>
        /// Add patient
        /// </summary>
        /// <param name="createUserCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddPatientAsync(
            CreatePatientCommand createUserCommand,
            CancellationToken cancellationToken)
        {
            var result = await Sender.Send(createUserCommand, cancellationToken);
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
            var user = await Sender.Send(new GetPatientQuery(){Id =id}, cancellationToken);

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
        /// Delete patient
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePatientAsync([FromBody] Guid id, CancellationToken cancellationToken)
        {
            await Sender.Send(new DeletePatientCommand() { Id = id }, cancellationToken);

            return Ok($"User with ID = {id} was deleted");
        }

       
    }
}
