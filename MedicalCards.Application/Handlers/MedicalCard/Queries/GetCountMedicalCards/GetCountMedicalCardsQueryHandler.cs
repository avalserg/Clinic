using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches;

namespace MedicalCards.Application.Handlers.MedicalCard.Queries.GetCountMedicalCards
{
    public class GetCountMedicalCardsQueryHandler : BaseCashedQuery<GetCountMedicalCardsQuery, int>
    {
        private readonly IBaseReadRepository<Domain.MedicalCard> _medicalCardRepository;



        public GetCountMedicalCardsQueryHandler(IBaseReadRepository<Domain.MedicalCard> userRepository, MedicalCardsCountMemoryCache cache) : base(cache)
        {
            _medicalCardRepository = userRepository;

        }


        public override async Task<int> SentQueryAsync(GetCountMedicalCardsQuery request, CancellationToken cancellationToken)
        {
            var count = await _medicalCardRepository.AsAsyncRead().CountAsync(cancellationToken);
            return count;
        }
    }
}
