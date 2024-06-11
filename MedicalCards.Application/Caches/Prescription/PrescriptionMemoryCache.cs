using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.DTOs.Prescription;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Caches.Prescription
{
    public class PrescriptionMemoryCache : BaseCache<Result<GetPrescriptionDto>> { };

}
