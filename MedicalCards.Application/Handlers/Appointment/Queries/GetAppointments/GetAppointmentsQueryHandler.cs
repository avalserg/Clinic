using AutoMapper;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches.Appointment;
using MedicalCards.Application.DTOs;
using MedicalCards.Application.DTOs.Appointment;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Handlers.Appointment.Queries.GetAppointments
{
    public class GetAppointmentsQueryHandler : BaseCashedQuery<GetAppointmentsQuery, Result<BaseListDto<GetAppointmentDto>>>
    {
        private readonly IBaseReadRepository<Domain.Appointment> _appointments;


        private readonly IMapper _mapper;

        public GetAppointmentsQueryHandler(
            IBaseReadRepository<Domain.Appointment> appointments,
            IMapper mapper,
           AppointmentsListMemoryCache cache) : base(cache)
        {

            _appointments = appointments;
            _mapper = mapper;
        }
        public override async Task<Result<BaseListDto<GetAppointmentDto>>> SentQueryAsync(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var query = _appointments.AsQueryable().Where(ListAppointmentsWhere.Where(request));


            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }

            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }
            // order by  last name
            query = query.OrderBy(e => e.PatientId);

            var entitiesResult = await _appointments.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _appointments.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetAppointmentDto[]>(entitiesResult);

            return new BaseListDto<GetAppointmentDto>
            {
                Items = items,
                TotalCount = entitiesCount,

            };
        }
    }
}
