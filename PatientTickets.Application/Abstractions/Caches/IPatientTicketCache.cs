using PatientTickets.Application.DTOs;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Application.Abstractions.Caches
{
    public interface IPatientTicketCache : IBaseCache<Result<GetPatientTicketDto>>
    {
    }
}
