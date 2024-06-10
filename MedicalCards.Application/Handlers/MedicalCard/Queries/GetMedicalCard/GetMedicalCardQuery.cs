using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.DTOs.MedicalCard;

namespace MedicalCards.Application.Handlers.MedicalCard.Queries.GetMedicalCard
{
    public class GetMedicalCardQuery : IQuery<GetMedicalCardDto>
    {
        public Guid Id { get; init; } = default!;
    }
}
