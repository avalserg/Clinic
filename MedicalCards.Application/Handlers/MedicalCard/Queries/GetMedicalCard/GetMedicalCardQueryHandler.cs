using AutoMapper;
using MedicalCards.Application.Abstractions.ExternalProviders;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches.MedicalCard;
using MedicalCards.Application.DTOs.MedicalCard;
using MedicalCards.Domain.Errors;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Handlers.MedicalCard.Queries.GetMedicalCard
{
    public class GetMedicalCardQueryHandler : BaseCashedQuery<GetMedicalCardQuery, Result<GetMedicalCardDto>>
    {
        private readonly IBaseReadRepository<Domain.MedicalCard> _medicalCard;
        private readonly IMapper _mapper;
        private readonly IManageUsersProviders _applicationUsersProviders;
        public GetMedicalCardQueryHandler(
            IBaseReadRepository<Domain.MedicalCard> medicalCard,
            IMapper mapper,
            MedicalCardMemoryCache cache, IManageUsersProviders applicationUsersProviders) : base(cache)
        {
            _mapper = mapper;
            _applicationUsersProviders = applicationUsersProviders;
            _medicalCard = medicalCard;
        }

        public override async Task<Result<GetMedicalCardDto>> SentQueryAsync(GetMedicalCardQuery request, CancellationToken cancellationToken)
        {
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
