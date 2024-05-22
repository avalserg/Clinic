using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.Caches;
using ManageUsers.Application.Caches.Doctors;

namespace ManageUsers.Application.Handlers.Doctor.Queries.GetCountDoctors
{
    public class GetCountDoctorsQueryHandler: BaseCashedQuery<GetCountDoctorsQuery, int>
    {
        private readonly IBaseReadRepository<Domain.Doctor> _userRepository;
        


        public GetCountDoctorsQueryHandler(IBaseReadRepository<Domain.Doctor> userRepository,DoctorsCountMemoryCache cache) : base(cache)
        {
            _userRepository = userRepository;
            
        }


        public override async Task<int> SentQueryAsync(GetCountDoctorsQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.AsAsyncRead().CountAsync(ListDoctorsWhere.Where(request), cancellationToken);
        }
    }
}
