namespace MedicalCards.Application.Handlers.Prescription
{
    public class ListPrescriptionsFilter
    {

        public Guid DoctorId { get; init; }
        public Guid PatientId { get; init; }
        public string? FreeText { get; init; }
    }
}
