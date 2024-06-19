using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.Abstractions.Persistence.Repository.Writing;
using MedicalCards.Application.Caches.Appointment;
using MedicalCards.Domain.Errors;
using MedicalCards.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace MedicalCards.Application.Handlers.Appointment.Commands.UpdateAppointmentHasPrescription
{
    public class UpdateAppointmentHasPrescriptionCommandHandler : ICommandHandler<UpdateAppointmentHasPrescriptionCommand, bool>
    {
        private readonly IBaseWriteRepository<Domain.Appointment> _appointments;
        private readonly AppointmentMemoryCache _appointmentMemoryCache;
        private readonly AppointmentsListMemoryCache _listCache;
        private readonly ILogger<UpdateAppointmentHasPrescriptionCommand> _logger;



        public UpdateAppointmentHasPrescriptionCommandHandler(
            IBaseWriteRepository<Domain.Appointment> appointments,

            AppointmentMemoryCache appointmentMemoryCache,
            AppointmentsListMemoryCache listCache,
            ILogger<UpdateAppointmentHasPrescriptionCommand> logger)
        {
            _appointments = appointments;

            _appointmentMemoryCache = appointmentMemoryCache;
            _listCache = listCache;
            _logger = logger;
        }
        public async Task<Result<bool>> Handle(UpdateAppointmentHasPrescriptionCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointments.AsAsyncRead().SingleOrDefaultAsync(pt => pt.Id == request.Id, cancellationToken);
            if (appointment is null)
            {
                return Result.Failure<bool>(
                    DomainErrors.Appointment.AppointmentNotFound(request.Id));
            }
            appointment.UpdateAppointmentHasPrescription(true, request.PrescriptionId);
            await _appointments.UpdateAsync(appointment, cancellationToken);
            _listCache.Clear();
            _appointmentMemoryCache.Clear();
            _logger.LogInformation($"Appointment {appointment.Id} has prescription updated.");
            return true;
        }
    }
}
