using Microsoft.Extensions.Caching.Distributed;
using PatientTickets.Application.Abstractions.Caches;

namespace PatientTickets.Infarastructure.DistributedCache;

public class PatientTicketsCountCache : BaseCache<int>, IPatientTicketsCountCache
{
    public PatientTicketsCountCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
};