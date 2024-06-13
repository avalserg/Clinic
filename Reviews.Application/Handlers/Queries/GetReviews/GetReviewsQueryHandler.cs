using AutoMapper;
using Reviews.Application.Abstractions.Caches;
using Reviews.Application.Abstractions.Persistence.Repository.Read;
using Reviews.Application.BaseRealizations;

using Reviews.Application.DTOs;
using Reviews.Domain.Entities;
using Reviews.Domain.Shared;

namespace Reviews.Application.Handlers.Queries.GetReviews
{
    public class GetReviewsQueryHandler : BaseCashedQuery<GetReviewsQuery, Result<BaseListDto<GetReviewDto>>>
    {
        private readonly IBaseReadRepository<Review> _reviews;
        private readonly IMapper _mapper;
        public GetReviewsQueryHandler(IBaseReadRepository<Review> reviews, IMapper mapper, IReviewsListCache cache) : base(cache)
        {
            _mapper = mapper;
            _reviews = reviews;
        }

        public override async Task<Result<BaseListDto<GetReviewDto>>> SentQueryAsync(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            var query = _reviews.AsQueryable().Where(ListReviewsWhere.Where(request));
            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }

            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }
            query = query.OrderBy(e => e.PatientId);
            var entitiesResult = await _reviews.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _reviews.AsAsyncRead().CountAsync(query, cancellationToken);
            var items = _mapper.Map<GetReviewDto[]>(entitiesResult);
            return new BaseListDto<GetReviewDto>
            {
                Items = items,
                TotalCount = entitiesCount,
            };
        }
    }
}
