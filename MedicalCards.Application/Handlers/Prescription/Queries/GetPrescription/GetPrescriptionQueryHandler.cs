using AutoMapper;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Service;
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

        private readonly ICurrentUserService _currentUserService;
        public GetPrescriptionQueryHandler(
            IBaseReadRepository<Domain.Prescription> prescriptionReadRepository,
            IMapper mapper,
            PrescriptionMemoryCache cache,
            ICurrentUserService currentUserService) : base(cache)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _prescriptionReadRepository = prescriptionReadRepository;
        }
        public override async Task<Result<GetPrescriptionDto>> SentQueryAsync(GetPrescriptionQuery request, CancellationToken cancellationToken)
        {

            //if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Doctor) &&
            //    !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin)&&)
            //{
            //    throw new ForbiddenException();
            //}
            var prescription = await _prescriptionReadRepository.AsAsyncRead().SingleOrDefaultAsync(pt => pt.Id == request.Id, cancellationToken);
            if (prescription is null)
            {
                return Result.Failure<GetPrescriptionDto>(
                    DomainErrors.Prescription.PrescriptionNotFound(request.Id));
            }

            return _mapper.Map<GetPrescriptionDto>(prescription);

        }
    }
}
