namespace PatientTickets.Application.Handlers;

public class ListPatientTicketFilter
{
    public Guid PatientId { get; init; }
    public Guid DoctorId { get; init; }
    public string? FreeText { get; init; }
}