using AutoMapper;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.Caches.ApplicationUserMemoryCache;
using ManageUsers.Application.DTOs.ApplicationUser;
using ManageUsers.Application.Utils;
using ManageUsers.Domain.Exceptions.Base;

namespace ManageUsers.Application.Handlers.ApplicationUser.Queries.GetApplicationUser
{
    public class GetApplicationUserByIdQueryHandler : BaseCashedQuery<GetApplicationUserQuery, GetApplicationUserDto>
    {
        private readonly IBaseReadRepository<Domain.ApplicationUser> _users;

        private readonly IMapper _mapper;


        public GetApplicationUserByIdQueryHandler(IBaseReadRepository<Domain.ApplicationUser> users, IMapper mapper, ApplicationUserMemoryCache cache) : base(cache)
        {
            _users = users;
            _mapper = mapper;
        }

        public override async Task<GetApplicationUserDto> SentQueryAsync(GetApplicationUserQuery request, CancellationToken cancellationToken)
        {

            var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.Login == request.Login, cancellationToken);
            if (user is null)
            {
                // TODO exception 
                //throw new DoctorNotFoundDomainException(request.Login);
            }
            if (!PasswordHashUtil.Verify(request.Password, user.PasswordHash))
            {
                throw new ForbiddenException();
            }
            return _mapper.Map<GetApplicationUserDto>(user);
        }
    }
}
