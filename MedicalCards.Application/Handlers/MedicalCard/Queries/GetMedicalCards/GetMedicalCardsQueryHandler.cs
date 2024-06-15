using AutoMapper;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Service;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches.MedicalCard;
using MedicalCards.Application.DTOs;
using MedicalCards.Application.DTOs.MedicalCard;
using MedicalCards.Domain.Enums;
using MedicalCards.Domain.Exceptions.Base;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Handlers.MedicalCard.Queries.GetMedicalCards;

internal class GetMedicalCardsQueryHandler : BaseCashedQuery<GetMedicalCardsQuery, Result<BaseListDto<GetMedicalCardDto>>>
{
    private readonly IBaseReadRepository<Domain.MedicalCard> _medicalCards;

    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetMedicalCardsQueryHandler(
        IBaseReadRepository<Domain.MedicalCard> medicalCards,
        IMapper mapper,
        MedicalCardsListMemoryCache cache, ICurrentUserService currentUserService) : base(cache)
    {

        _medicalCards = medicalCards;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public override async Task<Result<BaseListDto<GetMedicalCardDto>>> SentQueryAsync(GetMedicalCardsQuery request, CancellationToken cancellationToken)
    {
        // only doctors and admins can get list medical cards 
        var isAdmin = _currentUserService.UserInRole(ApplicationUserRolesEnum.Admin);
        var isDoctor = _currentUserService.UserInRole(ApplicationUserRolesEnum.Doctor);

        if (!isAdmin && !isDoctor)
        {
            throw new ForbiddenException();
        }
        var query = _medicalCards.AsQueryable().Where(ListMedicalCardsWhere.Where(request));


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

        var entitiesResult = await _medicalCards.AsAsyncRead().ToArrayAsync(query, cancellationToken);
        var entitiesCount = await _medicalCards.AsAsyncRead().CountAsync(query, cancellationToken);

        var items = _mapper.Map<GetMedicalCardDto[]>(entitiesResult);

        return new BaseListDto<GetMedicalCardDto>
        {
            Items = items,
            TotalCount = entitiesCount,

        };
    }
}