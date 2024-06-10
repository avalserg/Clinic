using MedicalCards.Application.Abstractions.Mappings;

namespace MedicalCards.Application.DTOs.MedicalCard
{
    public class CreateMedicalCardDto : IMapFrom<Domain.MedicalCard>
    {
        public Guid PatientId { get; init; } = default!;
    }
}
