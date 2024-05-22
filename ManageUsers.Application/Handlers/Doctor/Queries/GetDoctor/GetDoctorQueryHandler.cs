using AutoMapper;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.Caches;
using ManageUsers.Application.Caches.Doctors;
using ManageUsers.Application.DTOs.Doctor;
using ManageUsers.Domain.Exceptions;

namespace ManageUsers.Application.Handlers.Doctor.Queries.GetDoctor;

internal class GetDoctorQueryHandler : BaseCashedQuery<GetDoctorQuery, GetDoctorDto>
{
    private readonly IBaseReadRepository<Domain.Doctor> _users;

    private readonly IMapper _mapper;
    

    public GetDoctorQueryHandler(IBaseReadRepository<Domain.Doctor> users, IMapper mapper, DoctorMemoryCache cache) : base(cache)
    {
        _users = users;
        _mapper = mapper;
    }

    public override async Task<GetDoctorDto> SentQueryAsync(GetDoctorQuery request, CancellationToken cancellationToken)
    {
       
        var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (user is null)
        {
            throw new DoctorNotFoundDomainException(request.Id);
        }
       
        return _mapper.Map<GetDoctorDto>(user);
    }
}