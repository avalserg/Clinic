using AutoMapper;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.Caches.Administrator;
using ManageUsers.Application.DTOs.Admin;
using ManageUsers.Domain;
using ManageUsers.Domain.Exceptions;

namespace ManageUsers.Application.Handlers.Admin.Queries.GetAdmin
{
    public class GetAdminQueryHandler : BaseCashedQuery<GetAdminQuery, GetAdminDto>
    {
        private readonly IBaseReadRepository<Administrator> _users;

        private readonly IMapper _mapper;


        public GetAdminQueryHandler(IBaseReadRepository<Administrator> users, IMapper mapper, AdministratorMemoryCache cache) : base(cache)
        {
            _users = users;
            _mapper = mapper;
        }

        public override async Task<GetAdminDto> SentQueryAsync(GetAdminQuery request, CancellationToken cancellationToken)
        {

            var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (user is null)
            {
                throw new DoctorNotFoundDomainException(request.Id);
            }

            return _mapper.Map<GetAdminDto>(user);
        }
    }
}
