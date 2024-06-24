using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientTickets.Application.Abstractions.Caches;
using PatientTickets.Infarastructure.DistributedCache;

namespace PatientTickets.Infarastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDistributedCachesServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");

            })
                .AddSingleton<IPatientTicketCache, PatientTicketCache>()
                .AddSingleton<IPatientTicketsCountCache, PatientTicketsCountCache>()
                .AddSingleton<IPatientTicketsListCache, PatientTicketsListCache>()
                .AddSingleton<RedisService>()
                ;

        }
    }
}
