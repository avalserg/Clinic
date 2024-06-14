using AutoMapper;
using PatientTickets.Application.Abstractions.Messaging;
using PatientTickets.Application.Abstractions.Persistence.Repository.Read;
using PatientTickets.Domain.Entities;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Application.Handlers.Queries.GetBusyTimeWithTheDoctor
{
    internal class GetBusyTimeWithTheDoctorQueryHandler : IQueryHandler<GetBusyTimeWithTheDoctorQuery, string[]>
    {
        private readonly IBaseReadRepository<PatientTicket> _patientTickets;
        private readonly IMapper _mapper;

        public GetBusyTimeWithTheDoctorQueryHandler(IBaseReadRepository<PatientTicket> patientTickets, IMapper mapper
        )
        {
            _mapper = mapper;

            _patientTickets = patientTickets;
        }

        public async Task<Result<string[]>> Handle(GetBusyTimeWithTheDoctorQuery request, CancellationToken cancellationToken)
        {
            var query = _patientTickets.AsQueryable().Where(d => d.DateAppointment.Date == request.DateAppointment && d.DoctorId == request.DoctorId).Select(d => d.DateAppointment.ToShortTimeString());

            var entitiesResult = await _patientTickets.AsAsyncRead().ToArrayAsync(query, cancellationToken);

            return entitiesResult;
        }
    }
}