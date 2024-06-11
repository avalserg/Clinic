using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.DTOs;
using MedicalCards.Application.DTOs.Prescription;

namespace MedicalCards.Application.Handlers.Prescription.Queries.GetPrescriptions
{
    public class GetPrescriptionsQuery : ListPrescriptionsFilter, IBasePaginationFilter, IQuery<BaseListDto<GetPrescriptionDto>>
    {
        public int? Limit { get; init; }

        public int? Offset { get; init; }
    }
}
