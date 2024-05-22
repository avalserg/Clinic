using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.Caches;
using ManageUsers.Application.Caches.Patients;

namespace ManageUsers.Application.Handlers.Patient.Queries.GetCountPatients
{
    public class GetCountPatientsQueryHandler: BaseCashedQuery<GetCountPatientsQuery, int>
    {
        private readonly IBaseReadRepository<Domain.Patient> _userRepository;
        


        public GetCountPatientsQueryHandler(IBaseReadRepository<Domain.Patient> userRepository,PatientsCountMemoryCache cache) : base(cache)
        {
            _userRepository = userRepository;
            
        }


        public override async Task<int> SentQueryAsync(GetCountPatientsQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.AsAsyncRead().CountAsync(ListAdminWhere.Where(request), cancellationToken);
        }
    }
}
