using Reviews.Application.Abstractions.Messaging;
using Reviews.Application.DTOs;

namespace Reviews.Application.Handlers.Commands.CreateReview;


public sealed record CreateReviewCommand(
     Guid PatientId,
    string Description
   ) : ICommand<CreateReviewDto>;
