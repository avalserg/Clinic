using Microsoft.Extensions.Caching.Distributed;
using PatientTickets.Application.Abstractions.Caches;
using PatientTickets.Application.DTOs;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Infarastructure.DistributedCache;

public class PatientTicketsListCache : BaseCache<Result<BaseListDto<GetPatientTicketDto>>>, IPatientTicketsListCache
{
    public PatientTicketsListCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
};