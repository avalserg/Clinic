using PatientTickets.Application.Abstractions.Messaging;
using PatientTickets.Application.Abstractions.Persistence.Repository.Read;
using PatientTickets.Application.DTOs;
using PatientTickets.Domain.Entities;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Application.Handlers.Queries.GetCountPatientsPerDay
{
    internal class GetCountPatientsByAgeQueryHandler : IQueryHandler<GetCountPatientTicketsPerDayQuery, GetNumberPatientTicketsWithCountPerDayDto[]>
    {
        private readonly IBaseReadRepository<PatientTicket> _patientTicketRepository;



        public GetCountPatientsByAgeQueryHandler(IBaseReadRepository<PatientTicket> patientTicketRepository)
        {
            _patientTicketRepository = patientTicketRepository;

        }


        public async Task<Result<GetNumberPatientTicketsWithCountPerDayDto[]>> Handle(GetCountPatientTicketsPerDayQuery request, CancellationToken cancellationToken)
        {
            var query = _patientTicketRepository.AsQueryable()
                .GroupBy(p => p.DateAppointment.Hour)
                .Select(x => new GetNumberPatientTicketsWithCountPerDayDto
                {
                    Count = x.Count(),
                    HoursAppointment = x.Key
                });

            var entitiesResult = await _patientTicketRepository.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            return entitiesResult;
        }
    }


}

