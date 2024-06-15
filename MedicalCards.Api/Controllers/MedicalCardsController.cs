using MediatR;
using MedicalCards.Api.Abstractions;
using MedicalCards.Api.Contracts;
using MedicalCards.Application.Handlers.MedicalCard.Commands.DeleteMedicalCard;
using MedicalCards.Application.Handlers.MedicalCard.Queries.GetCountMedicalCards;
using MedicalCards.Application.Handlers.MedicalCard.Queries.GetMedicalCard;
using MedicalCards.Application.Handlers.MedicalCard.Queries.GetMedicalCards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCards.Api.Controllers
{
    [ApiController]
    [Authorize]
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

        [HttpGet("totalCount")]
        public async Task<IActionResult> GetCountMedicalCardsAsync(string? labelFreeText, CancellationToken cancellationToken)
        {

            var users = await Sender.Send(new GetCountMedicalCardsQuery() { FreeText = labelFreeText },
                cancellationToken);

            return Ok(users);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]

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
