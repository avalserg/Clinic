using MediatR;
using Reviews.Application.Abstractions.Messaging;
using Reviews.Application.DTOs;
using Reviews.Domain.Shared;

namespace Reviews.Application.Handlers.Queries.GetReview
{
    public class GetReviewQuery : IQuery<GetReviewDto>
    {
        public Guid Id { get; init; } = default!;
    }
}
