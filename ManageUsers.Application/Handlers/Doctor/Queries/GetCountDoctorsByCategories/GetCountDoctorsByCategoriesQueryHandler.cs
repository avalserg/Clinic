using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.DTOs.Doctor;
using ManageUsers.Domain.Shared;

namespace ManageUsers.Application.Handlers.Doctor.Queries.GetCountDoctorsByCategories
{
    internal class GetCountDoctorsByCategoriesQueryHandler : IQueryHandler<GetCountDoctorsByCategoriesQuery, GetDoctorsCategoriesWithCount[]>
    {
        private readonly IBaseReadRepository<Domain.Doctor> _userRepository;



        public GetCountDoctorsByCategoriesQueryHandler(IBaseReadRepository<Domain.Doctor> userRepository)
        {
            _userRepository = userRepository;

        }


        public async Task<Result<GetDoctorsCategoriesWithCount[]>> Handle(GetCountDoctorsByCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = _userRepository.AsQueryable()
                .GroupBy(p => p.Category)
                .Select(x => new GetDoctorsCategoriesWithCount()
                {
                    Count = x.Count(),
                    Category = x.Key
                });
            var entitiesResult = await _userRepository.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            return entitiesResult;
        }
    }


}

