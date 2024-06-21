using MediatR;
using MedicalCards.Api.Abstractions;
using MedicalCards.Application.Handlers.Appointment.Commands.CreateAppointment;
using MedicalCards.Application.Handlers.Appointment.Queries.GetAppointment;
using MedicalCards.Application.Handlers.Appointment.Queries.GetAppointments;
using MedicalCards.Application.Handlers.Appointment.Queries.GetCountAppointments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCards.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AppointmentsController : ApiController
    {
        public AppointmentsController(ISender sender) : base(sender)
        {
        }
        /// <summary>
        /// Get concrete appointment by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {

            var result = await Sender.Send(new GetAppointmentQuery() { Id = id }, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.Value);
        }

        /// <summary>
        /// Get all MedicalCards 
        /// </summary>
        /// <param name="getListAppointmentsQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetAllAppointmentsAsync(
            [FromQuery] GetAppointmentsQuery getListAppointmentsQuery,
            CancellationToken cancellationToken)
        {
            var result = await Sender.Send(getListAppointmentsQuery, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            var countAppointments = await Sender.Send(
                new GetCountAppointmentsQuery() { },
                cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", countAppointments.ToString());
            return Ok(result.Value);
        }

        /// <summary>
        /// Get count all medical cards
        /// </summary>
        /// <param name="labelFreeText"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("totalCount")]
        public async Task<IActionResult> GetCountAppointmentsAsync(string? labelFreeText, CancellationToken cancellationToken)
        {

            var countAppointments = await Sender.Send(new GetCountAppointmentsQuery() { FreeText = labelFreeText },
                cancellationToken);

            return Ok(countAppointments);
        }
        /// <summary>
        /// Add new appointment
        /// </summary>
        /// <param name="createAppointmentCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAppointmentAsync(
            CreateAppointmentCommand createAppointmentCommand,
            CancellationToken cancellationToken)
        {

            var result = await Sender.Send(createAppointmentCommand, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.Value);
        }
    }
}
