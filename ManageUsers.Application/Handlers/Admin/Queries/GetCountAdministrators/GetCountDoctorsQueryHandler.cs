using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.Caches.Administrator;
using ManageUsers.Domain;

namespace ManageUsers.Application.Handlers.Admin.Queries.GetCountAdministrators
{
    public class GetCountAdministratorsQueryHandler : BaseCashedQuery<GetCountAdministratorsQuery, int>
    {
        private readonly IBaseReadRepository<Administrator> _userRepository;



        public GetCountAdministratorsQueryHandler(IBaseReadRepository<Administrator> userRepository, AdministratorsCountMemoryCache cache) : base(cache)
        {
            _userRepository = userRepository;

        }


        public override async Task<int> SentQueryAsync(GetCountAdministratorsQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.AsAsyncRead().CountAsync(ListAdminWhere.Where(request), cancellationToken);
        }
    }
}
