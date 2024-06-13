using AutoMapper;
using Reviews.Application.Abstractions.Caches;
using Reviews.Application.Abstractions.Persistence.Repository.Read;
using Reviews.Application.BaseRealizations;

using Reviews.Application.DTOs;
using Reviews.Domain.Entities;
using Reviews.Domain.Errors;
using Reviews.Domain.Shared;

namespace Reviews.Application.Handlers.Queries.GetReview
{
    public class GetReviewQueryHandler : BaseCashedQuery<GetReviewQuery, Result<GetReviewDto>>
    {
        private readonly IBaseReadRepository<Review> _reviews;
        private readonly IMapper _mapper;
        public GetReviewQueryHandler(IBaseReadRepository<Review> reviews, IMapper mapper, IReviewCache cache) : base(cache)
        {
            _reviews = reviews;
            _mapper = mapper;
        }

        public override async Task<Result<GetReviewDto>> SentQueryAsync(GetReviewQuery request, CancellationToken cancellationToken)
        {
            var review = await _reviews.AsAsyncRead().SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
            if (review is null)
            {
                return Result.Failure<GetReviewDto>(DomainErrors.Review.NotFound(request.Id));
            }

            return _mapper.Map<GetReviewDto>(review);
        }
    }
}
