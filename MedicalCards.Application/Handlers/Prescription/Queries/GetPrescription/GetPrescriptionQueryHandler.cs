using AutoMapper;
using MedicalCards.Application.Abstractions.ExternalProviders;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.Caches.Prescription;
using MedicalCards.Application.DTOs.Prescription;
using MedicalCards.Domain.Errors;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Handlers.Prescription.Queries.GetPrescription
{
    public class GetPrescriptionQueryHandler : BaseCashedQuery<GetPrescriptionQuery, Result<GetPrescriptionDto>>
    {
        private readonly IBaseReadRepository<Domain.Prescription> _prescriptionReadRepository;
        private readonly IMapper _mapper;
        private readonly IManageUsersProviders _applicationUsersProviders;
        public GetPrescriptionQueryHandler(
            IBaseReadRepository<Domain.Prescription> prescriptionReadRepository,
            IMapper mapper,
            PrescriptionMemoryCache cache, IManageUsersProviders applicationUsersProviders) : base(cache)
        {
            _mapper = mapper;
            _applicationUsersProviders = applicationUsersProviders;
            _prescriptionReadRepository = prescriptionReadRepository;
        }
        public override async Task<Result<GetPrescriptionDto>> SentQueryAsync(GetPrescriptionQuery request, CancellationToken cancellationToken)
        {
            var prescription = await _prescriptionReadRepository.AsAsyncRead().SingleOrDefaultAsync(pt => pt.Id == request.Id, cancellationToken);
            if (prescription is null)
            {
                return Result.Failure<GetPrescriptionDto>(
                    DomainErrors.Prescription.PrescriptionNotFound(request.Id));
            }
            //var ownerMedicalCard = await _applicationUsersProviders.GetPatientByIdAsync(prescription.PatientId, cancellationToken);
            //if (ownerMedicalCard is null)
            //{
            //    // TODO Result
            //    throw new ArgumentException();
            //}

            return _mapper.Map<GetPrescriptionDto>(prescription);

        }
    }
}
