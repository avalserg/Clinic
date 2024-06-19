namespace MedicalCards.Application.Handlers.Appointment;

public class ListAppointmentsFilter
{
    public Guid DoctorId { get; init; }
    public Guid PatientId { get; init; }
    public string? FreeText { get; init; }
}