using PatientTickets.Application.BaseRealizations;
using PatientTickets.Application.DTOs;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Application.Caches;

public class PatientTicketMemoryCache : BaseCache<Result<GetPatientTicketDto>>;