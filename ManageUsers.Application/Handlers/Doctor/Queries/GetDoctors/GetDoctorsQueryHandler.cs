using AutoMapper;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.Caches.Doctors;
using ManageUsers.Application.DTOs;
using ManageUsers.Application.DTOs.Doctor;

namespace ManageUsers.Application.Handlers.Doctor.Queries.GetDoctors;

internal class GetDoctorsQueryHandler : BaseCashedQuery<GetDoctorsQuery, BaseListDto<GetDoctorDto>>
{
    private readonly IBaseReadRepository<Domain.Doctor> _users;
    private readonly IBaseReadRepository<Domain.ApplicationUser> _applicationUsers;

    private readonly IMapper _mapper;

    public GetDoctorsQueryHandler(IBaseReadRepository<Domain.Doctor> users, IBaseReadRepository<Domain.ApplicationUser> applicationUsers, IMapper mapper, DoctorsListMemoryCache cache) : base(cache)
    {
        _applicationUsers = applicationUsers;
        _users = users;
        _mapper = mapper;
    }

    public override async Task<BaseListDto<GetDoctorDto>> SentQueryAsync(GetDoctorsQuery request, CancellationToken cancellationToken)
    {
        var query = _users.AsQueryable().Where(ListDoctorsWhere.Where(request));


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

        var items = _mapper.Map<GetDoctorDto[]>(entitiesResult);
        return new BaseListDto<GetDoctorDto>
        {
            Items = items,
            TotalCount = entitiesCount
        };
    }
}