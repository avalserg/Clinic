using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.DTOs;
using MedicalCards.Application.DTOs.Prescription;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Caches.Prescription
{
    public class PrescriptionsListMemoryCache : BaseCache<Result<BaseListDto<GetPrescriptionDto>>>
    {
    }
}
