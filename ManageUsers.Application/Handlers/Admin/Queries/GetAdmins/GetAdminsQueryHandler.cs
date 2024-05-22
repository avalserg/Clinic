using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ManageUsers.Application.Abstractions;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.Caches;
using ManageUsers.Application.Caches.Administrator;
using ManageUsers.Application.DTOs;
using ManageUsers.Application.DTOs.Admin;
using ManageUsers.Application.DTOs.Patient;
using ManageUsers.Domain;

namespace ManageUsers.Application.Handlers.Admin.Queries.GetAdmins
{
    internal class GetAdminsQueryHandler : BaseCashedQuery<GetAdminsQuery, BaseListDto<GetAdminDto>>
    {
        private readonly IBaseReadRepository<Administrator> _adminRepository;
        private readonly IBaseReadRepository<Domain.ApplicationUser> _applicationUsers;

        private readonly IMapper _mapper;
        public GetAdminsQueryHandler(AdministratorsListMemoryCache cache, IBaseReadRepository<Administrator> adminRepository, IBaseReadRepository<Domain.ApplicationUser> applicationUsers, IMapper mapper) : base(cache)
        {
            _adminRepository = adminRepository;
            _applicationUsers = applicationUsers;
            _mapper = mapper;
        }

        public override async Task<BaseListDto<GetAdminDto>> SentQueryAsync(GetAdminsQuery request, CancellationToken cancellationToken)
        {
            var query = _adminRepository.AsQueryable().Where(ListAdminWhere.Where(request));


            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }

            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }

            query = query.OrderBy(e => e.ApplicationUserId);

            var entitiesResult = await _adminRepository.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _adminRepository.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetAdminDto[]>(entitiesResult);
            return new BaseListDto<GetAdminDto>
            {
                Items = items,
                TotalCount = entitiesCount,

            };
        }
    }
}
