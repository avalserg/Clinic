using Reviews.Application.Abstractions.Caches;
using Reviews.Application.Abstractions.Persistence.Repository.Read;
using Reviews.Application.BaseRealizations;

using Reviews.Domain.Entities;

namespace Reviews.Application.Handlers.Queries.GetCountReviews
{
    public class GetCountReviewsQueryHandler : BaseCashedQuery<GetCountReviewsQuery, int>
    {
        private readonly IBaseReadRepository<Review> _reviewRepository;



        public GetCountReviewsQueryHandler(IBaseReadRepository<Review> userRepository, IReviewsCountCache cache) : base(cache)
        {
            _reviewRepository = userRepository;

        }


        public override async Task<int> SentQueryAsync(GetCountReviewsQuery request, CancellationToken cancellationToken)
        {
            var count = await _reviewRepository.AsAsyncRead().CountAsync(cancellationToken);
            return count;
        }
    }
}
