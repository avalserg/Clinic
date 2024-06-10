using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.DTOs;
using MedicalCards.Application.DTOs.MedicalCard;

namespace MedicalCards.Application.Handlers.MedicalCard.Queries.GetMedicalCards;

public class GetMedicalCardsQuery : ListMedicalCardsFilter, IBasePaginationFilter, IQuery<BaseListDto<GetMedicalCardDto>>
{
    public int? Limit { get; init; }

    public int? Offset { get; init; }
}