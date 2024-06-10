using AutoMapper;
using MedicalCards.Application.Abstractions.ExternalProviders;
using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Persistence.Repository.Writing;
using MedicalCards.Application.Caches;
using MedicalCards.Application.DTOs.MedicalCard;
using MedicalCards.Domain.Errors;
using MedicalCards.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace MedicalCards.Application.Handlers.MedicalCard.Commands.CreateMedicalCard;

internal class CreateMedicalCardCommandHandler : ICommandHandler<CreateMedicalCardCommand, CreateMedicalCardDto>
{
    private readonly IBaseWriteRepository<Domain.MedicalCard> _writeMedicalCardRepository;
    private readonly IManageUsersProviders _applicationUsersProviders;
    private readonly IBaseReadRepository<Domain.MedicalCard> _readMedicalCardsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateMedicalCardCommandHandler> _logger;
    private readonly MedicalCardsListMemoryCache _listCache;
    private readonly MedicalCardsCountMemoryCache _countCache;
    private readonly MedicalCardMemoryCache _patientMemoryCache;

    public CreateMedicalCardCommandHandler(
        IBaseWriteRepository<Domain.MedicalCard> writeMedicalCardRepository,
        IBaseReadRepository<Domain.MedicalCard> readMedicalCardsRepository,
        IMapper mapper,
        ILogger<CreateMedicalCardCommandHandler> logger, IManageUsersProviders applicationUsersProviders,
        MedicalCardsListMemoryCache listCache,
        MedicalCardsCountMemoryCache countCache,
        MedicalCardMemoryCache patientMemoryCache)

    {

        _writeMedicalCardRepository = writeMedicalCardRepository;
        _mapper = mapper;
        _logger = logger;
        _readMedicalCardsRepository = readMedicalCardsRepository;
        _listCache = listCache;
        _countCache = countCache;
        _patientMemoryCache = patientMemoryCache;
        _applicationUsersProviders = applicationUsersProviders;

    }

    public async Task<Result<CreateMedicalCardDto>> Handle(CreateMedicalCardCommand request, CancellationToken cancellationToken)
    {
        var isMedicalCardExist = await _readMedicalCardsRepository.AsAsyncRead().AnyAsync(p => p.PatientId == request.PatientId, cancellationToken);
        if (isMedicalCardExist)
        {
            return Result.Failure<CreateMedicalCardDto>(DomainErrors.MedicalCard.MedicalCardAlreadyExist(request.PatientId));
        }
        var patient = await _applicationUsersProviders.GetPatientByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            // TODO Result
            throw new ArgumentException();
        }


        var newMedicalCardGuid = Guid.NewGuid();


        var medicalCard = Domain.MedicalCard.Create(
            newMedicalCardGuid,
            request.PatientId,
            patient.FirstName,
            patient.LastName,
            patient.Patronymic,
            patient.DateBirthday,
            patient.PhoneNumber,
            patient.Address
            );



        medicalCard = await _writeMedicalCardRepository.AddAsync(medicalCard, cancellationToken);


        _listCache.Clear();
        _countCache.Clear();
        _patientMemoryCache.Clear();
        _logger.LogInformation($"New user {newMedicalCardGuid} created.");

        return _mapper.Map<CreateMedicalCardDto>(medicalCard);
    }
}
