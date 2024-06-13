using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reviews.Domain.Shared;

namespace Reviews.Api.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected readonly ISender Sender;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        protected ApiController(ISender sender) => Sender = sender;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        protected IActionResult HandleFailure(Result result) =>
            result switch
            {
                { IsSuccess: true } => throw new InvalidOperationException(),
                IValidationResult validationResult =>
                    BadRequest(
                        CreateProblemDetails(
                            "Validation Error", StatusCodes.Status400BadRequest,
                            result.Error,
                            validationResult.Errors)),
                _ =>
                    BadRequest(
                        CreateProblemDetails(
                            "Bad Request",
                            StatusCodes.Status400BadRequest,
                            result.Error))
            };

        private static ProblemDetails CreateProblemDetails(
            string title,
            int status,
            Error error,
            Error[]? errors = null) =>
            new()
            {
                Title = title,
                Type = error.Code,
                Detail = error.Message,
                Status = status,
                Extensions = { { nameof(errors), errors } }
            };
    }

}
