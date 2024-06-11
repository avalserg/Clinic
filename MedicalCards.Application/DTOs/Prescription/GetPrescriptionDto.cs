using MedicalCards.Application.Abstractions.Mappings;

namespace MedicalCards.Application.DTOs.Prescription
{
    public class GetPrescriptionDto : IMapFrom<Domain.Prescription>
    {
        public Guid Id { get; init; } = default!;
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public Guid AppointmentId { get; set; }
        public string MedicineName { get; set; }
        public string ReleaseForm { get; set; }
        public string Amount { get; set; }
        public DateTime IssuingTime { get; set; }
        //public string DoctorFirstName { get; set; }
        //public string DoctorLastName { get; set; }
        //public string DoctorPatronymic { get; set; }

        //public string PatientFirstName { get; set; }
        //public string PatientLastName { get; set; }
        //public string PatientPatronymic { get; set; }
        //public string Speciality { get; set; }
    }
}
