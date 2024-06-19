using MedicalCards.Application.Abstractions.Messaging;

namespace MedicalCards.Application.Handlers.Appointment.Commands.UpdateAppointmentHasPrescription
{
    public class UpdateAppointmentHasPrescriptionCommand : ICommand<bool>
    {
        public Guid Id { get; init; }
        public string PrescriptionId { get; init; }
    }
}
