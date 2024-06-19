using PatientTickets.Application.Abstractions.Messaging;
using PatientTickets.Application.Abstractions.Persistence.Repository.Read;
using PatientTickets.Application.DTOs;
using PatientTickets.Domain.Entities;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Application.Handlers.Queries.GetCountPatientTicketsPerYearByMonths
{
    internal class GetCountPatientTicketsPerYearByMonthsQueryHandler : IQueryHandler<GetCountPatientTicketsPerYearByMonthsQuery, GetNumberPatientTicketsPerYearByMonthsDto[]>
    {
        private readonly IBaseReadRepository<PatientTicket> _patientTicketRepository;



        public GetCountPatientTicketsPerYearByMonthsQueryHandler(IBaseReadRepository<PatientTicket> patientTicketRepository)
        {
            _patientTicketRepository = patientTicketRepository;

        }
        public async Task<Result<GetNumberPatientTicketsPerYearByMonthsDto[]>> Handle(GetCountPatientTicketsPerYearByMonthsQuery request, CancellationToken cancellationToken)
        {
            var query = _patientTicketRepository.AsQueryable()
                .GroupBy(p => p.DateAppointment.Month)
                .Select(x => new GetNumberPatientTicketsPerYearByMonthsDto
                {
                    Count = x.Count(),
                    MonthsAppointment = x.Key

                });

            var entitiesResult = await _patientTicketRepository.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            return entitiesResult;
        }
    }
}
