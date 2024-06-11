using MedicalCards.Application.Abstractions.Mappings;

namespace MedicalCards.Application.DTOs.Appointment
{
    public class CreateAppointmentDto : IMapFrom<Domain.Appointment>
    {
        public Guid Id { get; init; } = default!;
        public Guid MedicalCardId { get; init; } = default!;
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }

        // history disease with patient`s complains
        public string DescriptionEpicrisis { get; set; }

        // methods research final diagnosis
        public string DescriptionAnamnesis { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorPatronymic { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientPatronymic { get; set; }
        public string Speciality { get; set; }
    }
}
