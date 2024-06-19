using MediatR;
using MedicalCards.Api.Abstractions;
using MedicalCards.Application.Handlers.Appointment.Commands.UpdateAppointmentHasPrescription;
using MedicalCards.Application.Handlers.Prescription.Commands.CreatePrescription;
using MedicalCards.Application.Handlers.Prescription.Queries.GetCountPrescriptions;
using MedicalCards.Application.Handlers.Prescription.Queries.GetPrescription;
using MedicalCards.Application.Handlers.Prescription.Queries.GetPrescriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCards.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class PrescriptionController : ApiController
    {
        public PrescriptionController(ISender sender) : base(sender)
        {
        }
        /// <summary>
        /// GetAllMedicalCardsAsync
        /// </summary>
        /// <param name="getListPatientsQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetAllPrescriptionsAsync(
            [FromQuery] GetPrescriptionsQuery getListPatientsQuery,
            CancellationToken cancellationToken)
        {
            var result = await Sender.Send(getListPatientsQuery, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            var countPrescriptions = await Sender.Send(
                new GetCountPrescriptionsQuery() { },
                cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", countPrescriptions.ToString());
            return Ok(result.Value);
        }
        /// <summary>
        /// Get concrete prescription by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescriptionByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {

            var result = await Sender.Send(new GetPrescriptionQuery() { Id = id }, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.Value);
        }
        /// <summary>
        /// Get count all medical cards
        /// </summary>
        /// <param name="labelFreeText"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        [HttpGet("totalCount")]
        public async Task<IActionResult> GetCountPrescriptionsAsync(string? labelFreeText, CancellationToken cancellationToken)
        {

            var countPrescriptions = await Sender.Send(new GetCountPrescriptionsQuery() { FreeText = labelFreeText },
                cancellationToken);

            return Ok(countPrescriptions);
        }
        /// <summary>
        /// Add new appointment
        /// </summary>
        /// <param name="createPrescriptionCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddPrescriptionAsync(
            CreatePrescriptionCommand createPrescriptionCommand,
            CancellationToken cancellationToken)
        {

            var result = await Sender.Send(createPrescriptionCommand, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            // update Appointment has prescription
            await Sender.Send(new UpdateAppointmentHasPrescriptionCommand() { Id = result.Value.AppointmentId, PrescriptionId = result.Value.Id.ToString() }, cancellationToken);
            return Ok(result.Value);
        }
    }
}
