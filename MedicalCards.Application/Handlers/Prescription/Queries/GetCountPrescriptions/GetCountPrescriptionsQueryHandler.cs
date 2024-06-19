using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Service;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches.MedicalCard;

namespace MedicalCards.Application.Handlers.Prescription.Queries.GetCountPrescriptions
{
    public class GetCountPrescriptionsQueryHandler : BaseCashedQuery<GetCountPrescriptionsQuery, int>
    {
        private readonly IBaseReadRepository<Domain.Prescription> _prescriptionRepository;
        private readonly ICurrentUserService _currentUserService;


        public GetCountPrescriptionsQueryHandler(IBaseReadRepository<Domain.Prescription> userRepository, MedicalCardsCountMemoryCache cache, ICurrentUserService currentUserService) : base(cache)
        {
            _prescriptionRepository = userRepository;
            _currentUserService = currentUserService;
        }


        public override async Task<int> SentQueryAsync(GetCountPrescriptionsQuery request, CancellationToken cancellationToken)
        {
            // only doctors and admins can view appointment 
            //if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Doctor) &&
            //    !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            //{
            //    throw new ForbiddenException();
            //}
            var count = await _prescriptionRepository.AsAsyncRead().CountAsync(cancellationToken);
            return count;
        }
    }
}
