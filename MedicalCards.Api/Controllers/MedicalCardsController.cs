using MediatR;
using MedicalCards.Api.Abstractions;
using MedicalCards.Api.Contracts;
using MedicalCards.Application.Handlers.MedicalCard.Commands.CreateMedicalCard;
using MedicalCards.Application.Handlers.MedicalCard.Commands.DeleteMedicalCard;
using MedicalCards.Application.Handlers.MedicalCard.Commands.UpdateMedicalCard;
using MedicalCards.Application.Handlers.MedicalCard.Queries.GetCountMedicalCards;
using MedicalCards.Application.Handlers.MedicalCard.Queries.GetMedicalCard;
using MedicalCards.Application.Handlers.MedicalCard.Queries.GetMedicalCards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace MedicalCards.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicalCardsController : ApiController
    {


        public MedicalCardsController(ISender sender) : base(sender) { }

        /// <summary>
        /// GetAllMedicalCardsAsync
        /// </summary>
        /// <param name="getListPatientsQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllMedicalCardsAsync(
            [FromQuery] GetMedicalCardsQuery getListPatientsQuery,
            CancellationToken cancellationToken)
        {
            var result = await Sender.Send(getListPatientsQuery, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            var countReviews = await Sender.Send(
                new GetCountMedicalCardsQuery() { },
                cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", countReviews.ToString());
            return Ok(result.Value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicalCardByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {

            var result = await Sender.Send(new GetMedicalCardQuery() { Id = id }, cancellationToken);
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
        [AllowAnonymous]
        [HttpGet("totalCount")]
        public async Task<IActionResult> GetCountMedicalCardsAsync(string? labelFreeText, CancellationToken cancellationToken)
        {

            var users = await Sender.Send(new GetCountMedicalCardsQuery() { FreeText = labelFreeText },
                cancellationToken);

            return Ok(users);
        }

        /// <summary>
        /// Add new medical card
        /// </summary>
        /// <param name="createPatientTicketRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddMedicalCardAsync(
            CreateMedicalCardCommand createPatientTicketRequest,
            CancellationToken cancellationToken)
        {

            var result = await Sender.Send(createPatientTicketRequest, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.Value);
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
        public async Task<IActionResult> UpdateMedicalCardAsync(
            [FromRoute] Guid id,
            [FromBody] UpdateMedicalCardRequest updatePatientRequest,
            CancellationToken cancellationToken)
        {
            // CultureInfo provider = CultureInfo.CurrentCulture;
            var command = new UpdateMedicalCardCommand(
                id,
                updatePatientRequest.FirstName,
                updatePatientRequest.LastName,
                updatePatientRequest.Patronymic,
                DateTime.Parse(updatePatientRequest.DateBirthday, null, DateTimeStyles.RoundtripKind),
                updatePatientRequest.Address,
                updatePatientRequest.PhoneNumber

            );
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.Value);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteMedicalCardAsync([FromBody] DeleteMedicalCardRequest request, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new DeleteMedicalCardCommand() { Id = Guid.Parse(request.Id) }, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok($"MedicalCard with ID = {request.Id} was deleted");
        }

    }
}
