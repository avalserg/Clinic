using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches.Appointment;

namespace MedicalCards.Application.Handlers.Appointment.Queries.GetCountAppointments
{
    public class GetCountAppointmentsQueryHandler : BaseCashedQuery<GetCountAppointmentsQuery, int>
    {
        private readonly IBaseReadRepository<Domain.Appointment> _appointmentRepository;



        public GetCountAppointmentsQueryHandler(IBaseReadRepository<Domain.Appointment> userRepository, AppointmentsCountMemoryCache cache) : base(cache)
        {
            _appointmentRepository = userRepository;

        }


        public override async Task<int> SentQueryAsync(GetCountAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var count = await _appointmentRepository.AsAsyncRead().CountAsync(cancellationToken);
            return count;
        }
    }
}
