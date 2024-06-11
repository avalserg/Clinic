
using AutoMapper;
using MedicalCards.Application.Abstractions.ExternalProviders;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches.Appointment;
using MedicalCards.Application.DTOs.Appointment;
using MedicalCards.Domain.Errors;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Handlers.Appointment.Queries.GetAppointment
{
    public class GetAppointmentQueryHandler : BaseCashedQuery<GetAppointmentQuery, Result<GetAppointmentDto>>
    {
        private readonly IBaseReadRepository<Domain.Appointment> _appointment;
        private readonly IMapper _mapper;
        private readonly IManageUsersProviders _applicationUsersProviders;
        public GetAppointmentQueryHandler(
            IBaseReadRepository<Domain.Appointment> appointment,
            IMapper mapper,
            AppointmentMemoryCache cache,
            IManageUsersProviders applicationUsersProviders) : base(cache)
        {
            _mapper = mapper;
            _applicationUsersProviders = applicationUsersProviders;
            _appointment = appointment;
        }

        public override async Task<Result<GetAppointmentDto>> SentQueryAsync(GetAppointmentQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _appointment.AsAsyncRead().SingleOrDefaultAsync(pt => pt.Id == request.Id, cancellationToken);
            if (appointment is null)
            {
                return Result.Failure<GetAppointmentDto>(
                    DomainErrors.Appointment.AppointmentNotFound(request.Id));
            }

            return _mapper.Map<GetAppointmentDto>(appointment);

        }
    }
}
