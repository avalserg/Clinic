using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.DTOs.Appointment;

namespace MedicalCards.Application.Handlers.Appointment.Commands.CreateAppointment;

public class CreateAppointmentCommand : ICommand<CreateAppointmentDto>
{
    public Guid MedicalCardId { get; init; }
    public string DescriptionEpicrisis { get; init; }
    public string DescriptionAnamnesis { get; init; }

    public Guid DoctorId { get; init; }

}

