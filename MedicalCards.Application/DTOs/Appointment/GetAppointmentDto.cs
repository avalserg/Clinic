using MedicalCards.Application.Abstractions.Mappings;

namespace MedicalCards.Application.DTOs.Appointment
{
    public class GetAppointmentDto : IMapFrom<Domain.Appointment>
    {
        public Guid Id { get; init; } = default!;
        public Guid MedicalCardId { get; init; } = default!;
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public Guid PrescriptionId { get; set; }

        // history disease with patient`s complains
        public string DescriptionEpicrisis { get; set; } = default!;

        // methods research final diagnosis
        public string DescriptionAnamnesis { get; set; } = default!;
        public string DoctorFirstName { get; set; } = default!;
        public string DoctorLastName { get; set; } = default!;
        public string DoctorPatronymic { get; set; } = default!;

        public string PatientFirstName { get; set; } = default!;
        public string PatientLastName { get; set; } = default!;
        public string PatientPatronymic { get; set; } = default!;
        public string Speciality { get; set; } = default!;
        public DateTime IssuingTime { get; set; }
        public bool HasPrescription { get; set; }

    }
}
