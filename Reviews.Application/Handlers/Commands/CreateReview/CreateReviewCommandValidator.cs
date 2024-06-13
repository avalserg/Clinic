using FluentValidation;

namespace Reviews.Application.Handlers.Commands.CreateReview;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(e => e.Description).MinimumLength(10).MaximumLength(1000).NotEmpty();
    }
}