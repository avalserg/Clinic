using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.DTOs.Prescription;

namespace MedicalCards.Application.Handlers.Prescription.Commands.CreatePrescription
{
    public class CreatePrescriptionCommand : ICommand<CreatePrescriptionDto>
    {
        public Guid PatientId { get; set; }
        public string MedicineName { get; set; }
        public string ReleaseForm { get; set; }
        public string Amount { get; set; }

    }
}
