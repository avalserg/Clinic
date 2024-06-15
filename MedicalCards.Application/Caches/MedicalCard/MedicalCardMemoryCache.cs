using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.DTOs.MedicalCard;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Caches.MedicalCard;

public class MedicalCardMemoryCache : BaseCache<Result<GetMedicalCardDto>>
{
};