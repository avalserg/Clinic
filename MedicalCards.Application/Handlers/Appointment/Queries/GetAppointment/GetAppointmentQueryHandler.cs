
using AutoMapper;
using MedicalCards.Application.Abstractions.ExternalProviders;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Service;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches.Appointment;
using MedicalCards.Application.DTOs.Appointment;
using MedicalCards.Domain.Enums;
using MedicalCards.Domain.Errors;
using MedicalCards.Domain.Exceptions.Base;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Handlers.Appointment.Queries.GetAppointment
{
    public class GetAppointmentQueryHandler : BaseCashedQuery<GetAppointmentQuery, Result<GetAppointmentDto>>
    {
        private readonly IBaseReadRepository<Domain.Appointment> _appointment;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public GetAppointmentQueryHandler(
            IBaseReadRepository<Domain.Appointment> appointment,
            IMapper mapper,
            AppointmentMemoryCache cache,
            IManageUsersProviders applicationUsersProviders, ICurrentUserService currentUserService) : base(cache)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _appointment = appointment;
        }

        public override async Task<Result<GetAppointmentDto>> SentQueryAsync(GetAppointmentQuery request, CancellationToken cancellationToken)
        {
            // only doctors and admins can view appointment 
            if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Doctor) &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }
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
