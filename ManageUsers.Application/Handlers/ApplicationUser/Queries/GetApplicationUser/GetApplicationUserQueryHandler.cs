using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.Caches.ApplicationUserMemoryCache;
using ManageUsers.Application.Caches.Doctors;
using ManageUsers.Application.DTOs.ApplicationUser;
using ManageUsers.Application.DTOs.Doctor;
using ManageUsers.Application.Handlers.Doctor.Queries.GetDoctor;
using ManageUsers.Domain.Exceptions;

namespace ManageUsers.Application.Handlers.ApplicationUser.Queries.GetApplicationUser
{
    public class GetApplicationUserQueryHandler : BaseCashedQuery<GetApplicationUserQuery, GetApplicationUserDto>
    {
        private readonly IBaseReadRepository<Domain.ApplicationUser> _users;

        private readonly IMapper _mapper;


        public GetApplicationUserQueryHandler(IBaseReadRepository<Domain.ApplicationUser> users, IMapper mapper, ApplicationUserMemoryCache cache) : base(cache)
        {
            _users = users;
            _mapper = mapper;
        }

        public override async Task<GetApplicationUserDto> SentQueryAsync(GetApplicationUserQuery request, CancellationToken cancellationToken)
        {

            var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == request.ApplicationUserId, cancellationToken);
            if (user is null)
            {
                throw new DoctorNotFoundDomainException(request.ApplicationUserId);
            }

            return _mapper.Map<GetApplicationUserDto>(user);
        }
    }
}
