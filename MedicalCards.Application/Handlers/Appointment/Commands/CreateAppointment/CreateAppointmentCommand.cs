using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.DTOs.Appointment;

namespace MedicalCards.Application.Handlers.Appointment.Commands.CreateAppointment;

public class CreateAppointmentCommand : ICommand<CreateAppointmentDto>
{
    public Guid PatientId { get; init; }
    public string DescriptionEpicrisis { get; init; } = default!;
    public string DescriptionAnamnesis { get; init; } = default!;

    public Guid DoctorId { get; init; }

}

