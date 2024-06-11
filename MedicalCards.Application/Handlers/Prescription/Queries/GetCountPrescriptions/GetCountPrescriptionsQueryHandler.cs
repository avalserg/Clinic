using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches.MedicalCard;

namespace MedicalCards.Application.Handlers.Prescription.Queries.GetCountPrescriptions
{
    public class GetCountPrescriptionsQueryHandler : BaseCashedQuery<GetCountPrescriptionsQuery, int>
    {
        private readonly IBaseReadRepository<Domain.Prescription> _prescriptionRepository;



        public GetCountPrescriptionsQueryHandler(IBaseReadRepository<Domain.Prescription> userRepository, MedicalCardsCountMemoryCache cache) : base(cache)
        {
            _prescriptionRepository = userRepository;

        }


        public override async Task<int> SentQueryAsync(GetCountPrescriptionsQuery request, CancellationToken cancellationToken)
        {
            var count = await _prescriptionRepository.AsAsyncRead().CountAsync(cancellationToken);
            return count;
        }
    }
}
