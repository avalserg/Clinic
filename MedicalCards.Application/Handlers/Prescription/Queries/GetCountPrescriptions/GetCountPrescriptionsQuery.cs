using MediatR;

namespace MedicalCards.Application.Handlers.Prescription.Queries.GetCountPrescriptions
{
    public class GetCountPrescriptionsQuery : ListPrescriptionsFilter, IRequest<int>
    {
    }
}
