using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reviews.Api.Abstractions;
using Reviews.Api.Contracts;
using Reviews.Application.Handlers.Commands.CreateReview;
using Reviews.Application.Handlers.Queries.GetCountReviews;
using Reviews.Application.Handlers.Queries.GetReview;
using Reviews.Application.Handlers.Queries.GetReviews;

namespace Reviews.Api.Controllers
{
    /// <summary>
    /// ReviewsController
    /// </summary>

    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class ReviewsController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="sender"></param>
        public ReviewsController(ISender sender) : base(sender)
        {

        }
        /// <summary>
        /// Get all reviews
        /// </summary>
        /// <param name="getListPatientsQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllReviewsAsync(
            [FromQuery] GetReviewsQuery getListPatientsQuery,
            CancellationToken cancellationToken)
        {
            var review = await Sender.Send(getListPatientsQuery, cancellationToken);
            if (review.IsFailure)
            {
                return HandleFailure(review);
            }
            var countReviews = await Sender.Send(
                new GetCountReviewsQuery() { },
                cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", countReviews.ToString());
            return Ok(review.Value);
        }
        /// <summary>
        /// Get certain review by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var review = await Sender.Send(new GetReviewQuery() { Id = id }, cancellationToken);
            if (review.IsFailure)
            {
                return HandleFailure(review);
            }
            return Ok(review.Value);
        }
        /// <summary>
        /// Create review 
        /// </summary>
        /// <param name="createReviewRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateReviewAsync(
            CreateReviewRequest createReviewRequest,
            CancellationToken cancellationToken)
        {
            var command = new CreateReviewCommand(
               createReviewRequest.PatientId,
               createReviewRequest.Description

            );
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Created($"reviews/{result.Value.Id}", result.Value);
        }
    }
}
