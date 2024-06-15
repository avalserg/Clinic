using AutoMapper;
using MedicalCards.Application.Abstractions.ExternalProviders;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Service;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches.MedicalCard;
using MedicalCards.Application.DTOs.MedicalCard;
using MedicalCards.Domain.Enums;
using MedicalCards.Domain.Errors;
using MedicalCards.Domain.Exceptions.Base;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Handlers.MedicalCard.Queries.GetMedicalCard
{
    public class GetMedicalCardQueryHandler : BaseCashedQuery<GetMedicalCardQuery, Result<GetMedicalCardDto>>
    {
        private readonly IBaseReadRepository<Domain.MedicalCard> _medicalCard;
        private readonly IMapper _mapper;
        private readonly IManageUsersProviders _applicationUsersProviders;
        private readonly ICurrentUserService _currentUserService;
        public GetMedicalCardQueryHandler(
            IBaseReadRepository<Domain.MedicalCard> medicalCard,
            IMapper mapper,
            MedicalCardMemoryCache cache, IManageUsersProviders applicationUsersProviders, ICurrentUserService currentUserService) : base(cache)
        {
            _mapper = mapper;
            _applicationUsersProviders = applicationUsersProviders;
            _currentUserService = currentUserService;
            _medicalCard = medicalCard;
        }

        public override async Task<Result<GetMedicalCardDto>> SentQueryAsync(GetMedicalCardQuery request, CancellationToken cancellationToken)
        {
            // only doctors and admins can get medical card
            if (
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin) &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Doctor))
            {
                throw new ForbiddenException();
            }
            var medicalCard = await _medicalCard.AsAsyncRead().SingleOrDefaultAsync(pt => pt.Id == request.Id, cancellationToken);
            if (medicalCard is null)
            {
                return Result.Failure<GetMedicalCardDto>(
                    DomainErrors.MedicalCard.MedicalCardNotFound(request.Id));
            }
            var ownerMedicalCard = await _applicationUsersProviders.GetPatientByIdAsync(medicalCard.PatientId, cancellationToken);
            if (ownerMedicalCard is null)
            {
                // TODO Result
                throw new ArgumentException();
            }

            return _mapper.Map<GetMedicalCardDto>(medicalCard);

        }
    }
}
