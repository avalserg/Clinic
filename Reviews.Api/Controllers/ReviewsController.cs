using ManageUsers.Api.Contracts.Patient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reviews.Api.Abstractions;
using Reviews.Application.Handlers.Commands.CreateReview;
using Reviews.Application.Handlers.Queries.GetCountReviews;
using Reviews.Application.Handlers.Queries.GetReview;
using Reviews.Application.Handlers.Queries.GetReviews;

namespace Reviews.Api.Controllers
{
    
    [Route("[controller]")]
    public class ReviewsController : ApiController
    {
       

        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(ILogger<ReviewsController> logger, ISender sender):base(sender)
        {
            _logger = logger;
        }

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
                new GetCountReviewsQuery() {},
                cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", countReviews.ToString());
            return Ok(review.Value);
        }
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
        [AllowAnonymous]
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
