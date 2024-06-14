namespace PatientTickets.Application.Handlers;

public class ListPatientTicketFilter
{
    public Guid PatientId { get; init; }
    public Guid DoctorId { get; init; }
    public DateTime DateAppointment { get; init; }
    public string? FreeText { get; init; }
}