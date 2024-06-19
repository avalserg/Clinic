using AutoMapper;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Service;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches.Appointment;
using MedicalCards.Application.DTOs;
using MedicalCards.Application.DTOs.Appointment;
using MedicalCards.Domain.Enums;
using MedicalCards.Domain.Exceptions.Base;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Handlers.Appointment.Queries.GetAppointments
{
    public class GetAppointmentsQueryHandler : BaseCashedQuery<GetAppointmentsQuery, Result<BaseListDto<GetAppointmentDto>>>
    {
        private readonly IBaseReadRepository<Domain.Appointment> _appointments;
        private readonly ICurrentUserService _currentUserService;

        private readonly IMapper _mapper;

        public GetAppointmentsQueryHandler(
            IBaseReadRepository<Domain.Appointment> appointments,
            IMapper mapper,
           AppointmentsListMemoryCache cache, ICurrentUserService currentUserService) : base(cache)
        {

            _appointments = appointments;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public override async Task<Result<BaseListDto<GetAppointmentDto>>> SentQueryAsync(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            // only doctors and admins can view appointment 
            if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Doctor) &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }
            var query = _appointments.AsQueryable().Where(ListAppointmentsWhere.Where(request));


            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }

            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }
            // order by  date appointment
            query = query.OrderBy(e => e.IssuingTime);

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
