using PatientTickets.Application.DTOs;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Application.Abstractions.Caches
{
    public interface IPatientTicketsListCache : IBaseCache<Result<BaseListDto<GetPatientTicketDto>>>
    {
    }
}
