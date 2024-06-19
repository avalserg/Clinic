using PatientTickets.Application.Abstractions.Messaging;
using PatientTickets.Application.DTOs;

namespace PatientTickets.Application.Handlers.Queries.GetCountPatientTicketsPerYearByMonths
{
    public class GetCountPatientTicketsPerYearByMonthsQuery : IQuery<GetNumberPatientTicketsPerYearByMonthsDto[]>
    {
    }
}
