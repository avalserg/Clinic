using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reviews.Application.Abstractions.Caches;
using Reviews.Infrastructure.DistributedCache;

namespace Reviews.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDistributedCachesServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");

            })
                .AddSingleton<IReviewCache, ReviewCache>()
                .AddSingleton<IReviewsCountCache, ReviewsCountCache>()
                .AddSingleton<IReviewsListCache, ReviewsListCache>()
                .AddSingleton<RedisService>()
                ;

        }
    }
}
