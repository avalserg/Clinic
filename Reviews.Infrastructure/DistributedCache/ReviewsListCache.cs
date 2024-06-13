using Microsoft.Extensions.Caching.Distributed;
using Reviews.Application.Abstractions.Caches;
using Reviews.Application.DTOs;
using Reviews.Domain.Shared;

namespace Reviews.Infrastructure.DistributedCache;

public class ReviewsListCache : BaseCache<Result<BaseListDto<GetReviewDto>>>, IReviewsListCache
{
    public ReviewsListCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
};