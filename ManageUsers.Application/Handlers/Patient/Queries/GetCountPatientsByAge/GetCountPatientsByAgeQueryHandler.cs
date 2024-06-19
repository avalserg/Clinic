using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.DTOs.Patient;
using ManageUsers.Domain.Shared;

namespace ManageUsers.Application.Handlers.Patient.Queries.GetCountPatientsByAge
{
    internal class GetCountPatientsByAgeQueryHandler : IQueryHandler<GetCountPatientsByAgeQuery, GetPatientsByAge[]>
    {
        private readonly IBaseReadRepository<Domain.Patient> _patientRepository;



        public GetCountPatientsByAgeQueryHandler(IBaseReadRepository<Domain.Patient> patientRepository)
        {
            _patientRepository = patientRepository;

        }


        public async Task<Result<GetPatientsByAge[]>> Handle(GetCountPatientsByAgeQuery request, CancellationToken cancellationToken)
        {
            var query = _patientRepository.AsQueryable()
                .GroupBy(p => p.DateBirthday.Year)
                .Select(x => new GetPatientsByAge
                {
                    Count = x.Count(),
                    Age = DateTime.Now.Year - x.Key
                });

            var entitiesResult = await _patientRepository.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            return entitiesResult;
        }
    }


}

