using Microsoft.Extensions.Caching.Distributed;
using Reviews.Application.Abstractions.Caches;
using Reviews.Application.DTOs;
using Reviews.Domain.Shared;

namespace Reviews.Infrastructure.DistributedCache;

public class ReviewCache : BaseCache<Result<GetReviewDto>>, IReviewCache
{
    public ReviewCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
};
