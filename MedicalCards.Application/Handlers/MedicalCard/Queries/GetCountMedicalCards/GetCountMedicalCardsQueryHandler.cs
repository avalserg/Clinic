using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Service;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches.MedicalCard;
using MedicalCards.Domain.Enums;
using MedicalCards.Domain.Exceptions.Base;

namespace MedicalCards.Application.Handlers.MedicalCard.Queries.GetCountMedicalCards
{
    public class GetCountMedicalCardsQueryHandler : BaseCashedQuery<GetCountMedicalCardsQuery, int>
    {
        private readonly IBaseReadRepository<Domain.MedicalCard> _medicalCardRepository;
        private readonly ICurrentUserService _currentUserService;


        public GetCountMedicalCardsQueryHandler(IBaseReadRepository<Domain.MedicalCard> userRepository, MedicalCardsCountMemoryCache cache, ICurrentUserService currentUserService) : base(cache)
        {
            _medicalCardRepository = userRepository;
            _currentUserService = currentUserService;
        }


        public override async Task<int> SentQueryAsync(GetCountMedicalCardsQuery request, CancellationToken cancellationToken)
        {
            // only doctors and admins can get count medical card 
            if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin)
                && !_currentUserService.UserInRole(ApplicationUserRolesEnum.Doctor))
            {
                throw new ForbiddenException();
            }
            var count = await _medicalCardRepository.AsAsyncRead().CountAsync(cancellationToken);
            return count;
        }
    }
}
