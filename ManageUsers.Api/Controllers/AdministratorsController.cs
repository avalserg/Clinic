using ManageUsers.Api.Abstractions;
using ManageUsers.Api.Contracts.Admin;
using ManageUsers.Application.Handlers.Admin.Commands.UpdateAdmin;
using ManageUsers.Application.Handlers.Admin.Queries.GetAdmin;
using ManageUsers.Application.Handlers.Admin.Queries.GetAdmins;
using ManageUsers.Application.Handlers.Admin.Queries.GetCountAdministrators;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageUsers.Api.Controllers
{
    [Route("[controller]")]
    public class AdministratorsController : ApiController
    {
        public AdministratorsController(ISender sender) : base(sender)
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
        public async Task<IActionResult> GetAdminByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var user = await Sender.Send(new GetAdminQuery() { Id = id }, cancellationToken);

            return Ok(user);
        }
        /// <summary>
        /// Get info about all admins in system
        /// </summary>
        /// <param name="getListPatientsQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAdminsAsync(
            [FromQuery] GetAdminsQuery getListPatientsQuery,
            CancellationToken cancellationToken)
        {
            var users = await Sender.Send(getListPatientsQuery, cancellationToken);
            var countPatients = await Sender.Send(
                new GetCountAdministratorsQuery() { FreeText = getListPatientsQuery.FreeText },
                cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", countPatients.ToString());
            return Ok(users);
        }
        /// <summary>
        /// Update info about Admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDoctorRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateAdministratorAsync(
            [FromRoute] Guid id,
            [FromBody] UpdateAdministratorRequest updateDoctorRequest,
            CancellationToken cancellationToken)
        {
            // CultureInfo provider = CultureInfo.CurrentCulture;
            var command = new UpdateAdminCommand(
                id,
                updateDoctorRequest.FirstName,
                updateDoctorRequest.LastName,
                updateDoctorRequest.Patronymic

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
