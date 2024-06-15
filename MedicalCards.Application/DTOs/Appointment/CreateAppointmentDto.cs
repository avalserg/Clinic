using MedicalCards.Application.Abstractions.Mappings;

namespace MedicalCards.Application.DTOs.Appointment
{
    public class CreateAppointmentDto : IMapFrom<Domain.Appointment>
    {
        public Guid Id { get; init; }
        public Guid MedicalCardId { get; init; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }

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
    }
}
