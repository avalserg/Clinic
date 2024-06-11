using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.DTOs.Prescription;

namespace MedicalCards.Application.Handlers.Prescription.Queries.GetPrescription
{
    public class GetPrescriptionQuery : IQuery<GetPrescriptionDto>
    {
        public Guid Id { get; init; } = default!;
    }
}
