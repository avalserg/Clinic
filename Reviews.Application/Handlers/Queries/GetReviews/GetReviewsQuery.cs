using Reviews.Application.Abstractions.Messaging;
using Reviews.Application.DTOs;

namespace Reviews.Application.Handlers.Queries.GetReviews
{
    public class GetReviewsQuery: ListReviewFilter, IBasePaginationFilter, IQuery<BaseListDto<GetReviewDto>>
    {
        public int? Limit { get; init; }
        public int? Offset { get; init; }
    }
}
