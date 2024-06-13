using Microsoft.Extensions.Caching.Distributed;
using Reviews.Application.Abstractions.Caches;

namespace Reviews.Infrastructure.DistributedCache;

public class ReviewsCountCache : BaseCache<int>, IReviewsCountCache
{
    public ReviewsCountCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
};