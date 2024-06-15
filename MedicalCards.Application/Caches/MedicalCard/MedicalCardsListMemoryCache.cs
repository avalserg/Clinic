using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.DTOs;
using MedicalCards.Application.DTOs.MedicalCard;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Caches.MedicalCard;

public class MedicalCardsListMemoryCache : BaseCache<Result<BaseListDto<GetMedicalCardDto>>>
{
};