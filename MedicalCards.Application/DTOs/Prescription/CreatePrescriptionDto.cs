using MedicalCards.Application.Abstractions.Mappings;

namespace MedicalCards.Application.DTOs.Prescription
{
    public class CreatePrescriptionDto : IMapFrom<Domain.Prescription>
    {
        public Guid Id { get; init; } = default!;
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public Guid AppointmentId { get; set; }
        public string MedicineName { get; set; } = default!;
        public string ReleaseForm { get; set; } = default!;
        public string Amount { get; set; } = default!;
        public DateTime IssuingTime { get; set; }

    }
}
