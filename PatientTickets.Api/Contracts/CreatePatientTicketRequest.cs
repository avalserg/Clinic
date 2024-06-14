namespace PatientTickets.Api.Contracts
{
    public sealed record CreatePatientTicketRequest(
        Guid PatientId,
        DateTime DateAppointment,
        Guid DoctorId,
        string HoursAppointment,
        string MinutesAppointment
        );
}
