using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientTickets.Api.Abstractions;
using PatientTickets.Api.Contracts;
using PatientTickets.Application.Handlers.Commands.CreatePatientTicket;
using PatientTickets.Application.Handlers.Commands.DeletePatientTicket;
using PatientTickets.Application.Handlers.Commands.UpdatePatientTicketHasVisit;
using PatientTickets.Application.Handlers.Queries.GetBusyTimeWithTheDoctor;
using PatientTickets.Application.Handlers.Queries.GetCountPatientsPerDay;
using PatientTickets.Application.Handlers.Queries.GetCountPatientTickets;
using PatientTickets.Application.Handlers.Queries.GetCountPatientTicketsPerYearByMonths;
using PatientTickets.Application.Handlers.Queries.GetPatientTicket;
using PatientTickets.Application.Handlers.Queries.GetPatientTickets;

namespace PatientTickets.Api.Controllers
{

    [Route("[controller]")]
    public class PatientTicketsController : ApiController
    {


        public PatientTicketsController(ISender sender) : base(sender) { }

        /// <summary>
        /// Get All Patient Tickets
        /// </summary>
        /// <param name="getListPatientsQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllPatientTicketsAsync(
            [FromQuery] GetPatientTicketsQuery getListPatientsQuery,
            CancellationToken cancellationToken)
        {
            var result = await Sender.Send(getListPatientsQuery, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            var countReviews = await Sender.Send(
                new GetCountPatientTicketsQuery() { },
                cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", countReviews.ToString());
            return Ok(result.Value);
        }
        /// <summary>
        /// Get patient ticket by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientTicketByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new GetPatientTicketQuery() { Id = id }, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.Value);
        }
        /// <summary>
        /// Add patient ticket
        /// </summary>
        /// <param name="createPatientTicketRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPatientTicketAsync(
            CreatePatientTicketRequest createPatientTicketRequest,

            CancellationToken cancellationToken)
        {
            var command = new CreatePatientTicketCommand(
                createPatientTicketRequest.PatientId,
                createPatientTicketRequest.DateAppointment,
                createPatientTicketRequest.DoctorId,
                createPatientTicketRequest.HoursAppointment,
                createPatientTicketRequest.MinutesAppointment
            );
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Created($"patientTickets/{result.Value.Id}", result.Value);
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

            var users = await Sender.Send(new GetCountPatientTicketsQuery() { FreeText = labelFreeText },
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
        [HttpGet("countOnTimePerDay")]
        public async Task<IActionResult> GetCountPatientTicketsOnTimePerDayAsync(CancellationToken cancellationToken)
        {

            var count = await Sender.Send(new GetCountPatientTicketsPerDayQuery(), cancellationToken);
            return Ok(count);
        }
        /// <summary>
        /// Count all doctors
        /// </summary>
        /// <param name="labelFreeText"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("countOnMonthPerYear")]
        public async Task<IActionResult> GetCountPatientTicketsPerYearByMonthsAsync(CancellationToken cancellationToken)
        {

            var count = await Sender.Send(new GetCountPatientTicketsPerYearByMonthsQuery(), cancellationToken);
            return Ok(count);
        }
        /// <summary>
        ///  Get busy time patient tickets by date to concrete doctor
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{doctorId}/dateAppointment")]
        public async Task<IActionResult> GetPatientTicketsTimeWithTheDoctorByDateAsync(Guid doctorId, [FromQuery] GetDateRequest request, CancellationToken cancellationToken)
        {

            var time = await Sender.Send(new GetBusyTimeWithTheDoctorQuery() { DateAppointment = request.DateAppointment, DoctorId = doctorId },
                cancellationToken);

            return Ok(time.Value);
        }

        /// <summary>
        /// Update patient ticket as visited
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPatch("{id}/IsVisited")]
        public async Task<IActionResult> UpdatePatientVisitToTheDoctorAsync(Guid id, CancellationToken cancellationToken)
        {
            var isVisited = await Sender.Send(new UpdatePatientTicketHasVisitCommand() { Id = id }, cancellationToken);
            return Ok(isVisited.Value);

        }
        /// <summary>
        /// Delete patient ticket
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePatientTicketAsync([FromBody] DeletePatientTicketRequest request, CancellationToken cancellationToken)
        {
            await Sender.Send(new DeletePatientTicketCommand() { Id = Guid.Parse(request.Id) }, cancellationToken);

            return Ok($"PatientTicket with ID = {request.Id} was deleted");
        }
    }
}
