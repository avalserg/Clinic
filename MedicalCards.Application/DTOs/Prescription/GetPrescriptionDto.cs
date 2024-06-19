using MedicalCards.Application.Abstractions.Mappings;

namespace MedicalCards.Application.DTOs.Prescription
{
    public class GetPrescriptionDto : IMapFrom<Domain.Prescription>
    {
        public Guid Id { get; init; } = default!;
        public Guid DoctorId { get; set; }
        public string DoctorFirstName { get; set; } = default!;
        public string DoctorLastName { get; set; } = default!;
        public string DoctorPatronymic { get; set; } = default!;

        public string DescriptionEpicrisis { get; set; } = default!;


        public string DescriptionAnamnesis { get; set; } = default!;
        public Guid PatientId { get; set; }
        public string PatientFirstName { get; set; } = default!;
        public string PatientLastName { get; set; } = default!;
        public string PatientPatronymic { get; set; } = default!;
        public Guid AppointmentId { get; set; }
        public string MedicineName { get; set; } = default!;
        public string ReleaseForm { get; set; } = default!;
        public string Amount { get; set; } = default!;
        public DateTime IssuingTime { get; set; }


    }
}
