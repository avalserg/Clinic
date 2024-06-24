using Microsoft.Extensions.Caching.Distributed;
using PatientTickets.Application.Abstractions.Caches;
using PatientTickets.Application.DTOs;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Infarastructure.DistributedCache;

public class PatientTicketCache : BaseCache<Result<GetPatientTicketDto>>, IPatientTicketCache
{
    public PatientTicketCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
};
