using AutoMapper;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.Caches.Patients;
using ManageUsers.Application.DTOs;
using ManageUsers.Application.DTOs.Patient;

namespace ManageUsers.Application.Handlers.Patient.Queries.GetPatients;

internal class GetPatientsQueryHandler : BaseCashedQuery<GetPatientsQuery, BaseListDto<GetPatientDto>>
{
    private readonly IBaseReadRepository<Domain.Patient> _users;
    private readonly IBaseReadRepository<Domain.ApplicationUser> _applicationUsers;

    private readonly IMapper _mapper;

    public GetPatientsQueryHandler(IBaseReadRepository<Domain.Patient> users, IBaseReadRepository<Domain.ApplicationUser> applicationUsers, IMapper mapper, PatientsListMemoryCache cache) : base(cache)
    {
        _applicationUsers = applicationUsers;
        _users = users;
        _mapper = mapper;
    }

    public override async Task<BaseListDto<GetPatientDto>> SentQueryAsync(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        var query = _users.AsQueryable().Where(ListAdminWhere.Where(request));


        if (request.Offset.HasValue)
        {
            query = query.Skip(request.Offset.Value);
        }

        if (request.Limit.HasValue)
        {
            query = query.Take(request.Limit.Value);
        }

        query = query.OrderBy(e => e.ApplicationUserId);

        var entitiesResult = await _users.AsAsyncRead().ToArrayAsync(query, cancellationToken);
        var entitiesCount = await _users.AsAsyncRead().CountAsync(query, cancellationToken);

        var items = _mapper.Map<GetPatientDto[]>(entitiesResult);
        return new BaseListDto<GetPatientDto>
        {
            Items = items,
            TotalCount = entitiesCount,

        };
    }
}