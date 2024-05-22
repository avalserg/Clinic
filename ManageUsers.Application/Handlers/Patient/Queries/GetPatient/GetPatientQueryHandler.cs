using AutoMapper;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.Caches.Patients;
using ManageUsers.Application.DTOs.Patient;
using ManageUsers.Domain.Exceptions;

namespace ManageUsers.Application.Handlers.Patient.Queries.GetPatient;

internal class GetPatientQueryHandler : BaseCashedQuery<GetPatientQuery, GetPatientDto>
{
    private readonly IBaseReadRepository<Domain.Patient> _users;

    private readonly IMapper _mapper;
    

    public GetPatientQueryHandler(IBaseReadRepository<Domain.Patient> users, IMapper mapper, PatientMemoryCache cache) : base(cache)
    {
        _users = users;
        _mapper = mapper;
    }

    public override async Task<GetPatientDto> SentQueryAsync(GetPatientQuery request, CancellationToken cancellationToken)
    {
       
        var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (user is null)
        {
            throw new PatientNotFoundDomainException(request.Id);
        }
       
        return _mapper.Map<GetPatientDto>(user);
    }
}