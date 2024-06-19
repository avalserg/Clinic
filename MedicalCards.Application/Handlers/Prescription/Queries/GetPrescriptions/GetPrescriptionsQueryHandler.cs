using AutoMapper;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Service;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches.Prescription;
using MedicalCards.Application.DTOs;
using MedicalCards.Application.DTOs.Prescription;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Handlers.Prescription.Queries.GetPrescriptions
{
    public class GetPrescriptionsQueryHandler : BaseCashedQuery<GetPrescriptionsQuery, Result<BaseListDto<GetPrescriptionDto>>>
    {
        private readonly IBaseReadRepository<Domain.Prescription> _prescriptionsReadRepository;

        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetPrescriptionsQueryHandler(
            IBaseReadRepository<Domain.Prescription> prescriptionsReadRepository,
            IMapper mapper,
            PrescriptionsListMemoryCache cache, ICurrentUserService currentUserService) : base(cache)
        {

            _prescriptionsReadRepository = prescriptionsReadRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public override async Task<Result<BaseListDto<GetPrescriptionDto>>> SentQueryAsync(GetPrescriptionsQuery request, CancellationToken cancellationToken)
        {
            // only doctors and admins can view appointment 
            //if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Doctor) &&
            //    !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            //{
            //    throw new ForbiddenException();
            //}
            var query = _prescriptionsReadRepository.AsQueryable().Where(ListPrescriptionsWhere.Where(request));


            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }

            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }
            // order by  last name
            query = query.OrderBy(e => e.PatientId);

            var entitiesResult = await _prescriptionsReadRepository.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _prescriptionsReadRepository.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetPrescriptionDto[]>(entitiesResult);

            return new BaseListDto<GetPrescriptionDto>
            {
                Items = items,
                TotalCount = entitiesCount,

            };
        }
    }
}
