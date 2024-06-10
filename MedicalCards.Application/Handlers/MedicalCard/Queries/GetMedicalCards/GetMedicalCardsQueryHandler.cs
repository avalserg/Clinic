using AutoMapper;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches;
using MedicalCards.Application.DTOs;
using MedicalCards.Application.DTOs.MedicalCard;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Handlers.MedicalCard.Queries.GetMedicalCards;

internal class GetMedicalCardsQueryHandler : BaseCashedQuery<GetMedicalCardsQuery, Result<BaseListDto<GetMedicalCardDto>>>
{
    private readonly IBaseReadRepository<Domain.MedicalCard> _users;


    private readonly IMapper _mapper;

    public GetMedicalCardsQueryHandler(
        IBaseReadRepository<Domain.MedicalCard> users,
        IMapper mapper,
        MedicalCardsListMemoryCache cache) : base(cache)
    {

        _users = users;
        _mapper = mapper;
    }

    public override async Task<Result<BaseListDto<GetMedicalCardDto>>> SentQueryAsync(GetMedicalCardsQuery request, CancellationToken cancellationToken)
    {
        var query = _users.AsQueryable().Where(ListMedicalCardsWhere.Where(request));


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

        var entitiesResult = await _users.AsAsyncRead().ToArrayAsync(query, cancellationToken);
        var entitiesCount = await _users.AsAsyncRead().CountAsync(query, cancellationToken);

        var items = _mapper.Map<GetMedicalCardDto[]>(entitiesResult);

        return new BaseListDto<GetMedicalCardDto>
        {
            Items = items,
            TotalCount = entitiesCount,

        };
    }
}