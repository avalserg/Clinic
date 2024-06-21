using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Service;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches.Appointment;

namespace MedicalCards.Application.Handlers.Appointment.Queries.GetCountAppointments
{
    public class GetCountAppointmentsQueryHandler : BaseCashedQuery<GetCountAppointmentsQuery, int>
    {
        private readonly IBaseReadRepository<Domain.Appointment> _appointmentRepository;
        private readonly ICurrentUserService _currentUserService;


        public GetCountAppointmentsQueryHandler(IBaseReadRepository<Domain.Appointment> userRepository, AppointmentsCountMemoryCache cache, ICurrentUserService currentUserService) : base(cache)
        {
            _appointmentRepository = userRepository;
            _currentUserService = currentUserService;
        }


        public override async Task<int> SentQueryAsync(GetCountAppointmentsQuery request, CancellationToken cancellationToken)
        {
            // only doctors and admins can get count appointment 
            //if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Doctor) &&
            //    !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            //{
            //    throw new ForbiddenException();
            //}
            var count = await _appointmentRepository.AsAsyncRead().CountAsync(cancellationToken);
            return count;
        }
    }
}
