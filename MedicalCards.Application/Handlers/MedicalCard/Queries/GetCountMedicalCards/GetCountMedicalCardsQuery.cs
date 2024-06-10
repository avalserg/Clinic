using MediatR;

namespace MedicalCards.Application.Handlers.MedicalCard.Queries.GetCountMedicalCards
{
    public class GetCountMedicalCardsQuery : ListMedicalCardsFilter, IRequest<int>
    {
    }
}
