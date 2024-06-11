using AutoMapper;
using MedicalCards.Application.Abstractions.ExternalProviders;
using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.Abstractions.Persistence.Repository.Writing;
using MedicalCards.Application.Abstractions.Service;
using MedicalCards.Application.Caches.MedicalCard;
using MedicalCards.Application.DTOs.MedicalCard;
using MedicalCards.Application.Handlers.MedicalCard.Commands.CreateMedicalCard;
using MedicalCards.Domain.Enums;
using MedicalCards.Domain.Errors;
using MedicalCards.Domain.Exceptions.Base;
using MedicalCards.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace MedicalCards.Application.Handlers.MedicalCard.Commands.UpdateMedicalCard;

internal class UpdateMedicalCardCommandHandler : ICommandHandler<UpdateMedicalCardCommand, GetMedicalCardDto>
{
    private readonly IBaseWriteRepository<Domain.MedicalCard> _medicalCards;
    private readonly IMapper _mapper;
    private readonly MedicalCardsListMemoryCache _listCache;
    private readonly ILogger<CreateMedicalCardCommandHandler> _logger;
    private readonly MedicalCardsCountMemoryCache _countCache;
    private readonly MedicalCardMemoryCache _medicalCardMemoryCache;
    private readonly ICurrentUserService _currentUserService;
    private readonly IManageUsersProviders _applicationUsersProviders;
    public UpdateMedicalCardCommandHandler(
        IBaseWriteRepository<Domain.MedicalCard> medicalCards,
        IMapper mapper,
        MedicalCardsListMemoryCache listCache,
        ILogger<CreateMedicalCardCommandHandler> logger,
        MedicalCardsCountMemoryCache countCache,
        MedicalCardMemoryCache medicalCardMemoryCache,
        ICurrentUserService currentUserService, IManageUsersProviders applicationUsersProviders)
    {

        _medicalCards = medicalCards;
        _mapper = mapper;
        _listCache = listCache;
        _logger = logger;
        _countCache = countCache;
        _medicalCardMemoryCache = medicalCardMemoryCache;
        _currentUserService = currentUserService;
        _applicationUsersProviders = applicationUsersProviders;
    }

    public async Task<Result<GetMedicalCardDto>> Handle(UpdateMedicalCardCommand request, CancellationToken cancellationToken)
    {
        // only Admin can update info
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }
        var medicalCard = await _medicalCards.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (medicalCard is null)
        {
            return Result.Failure<GetMedicalCardDto>(DomainErrors.MedicalCard.MedicalCardNotFound(request.Id));

        }
        var ownerMedicalCard = await _applicationUsersProviders.GetPatientByIdAsync(medicalCard.PatientId, cancellationToken);
        if (ownerMedicalCard is null)
        {
            return Result.Failure<GetMedicalCardDto>(DomainErrors.MedicalCard.MedicalCardPatientNotFound(medicalCard.PatientId));
        }


        medicalCard.UpdateOwnerMedicalCardInfo(
            request.FirstName,
            request.LastName,
            request.Patronymic,
            request.DateBirthday,
            request.PhoneNumber,
            request.Address
            );

        medicalCard = await _medicalCards.UpdateAsync(medicalCard, cancellationToken);


        _listCache.Clear();
        _countCache.Clear();
        _medicalCardMemoryCache.Clear();
        _logger.LogInformation($"Medical Card {medicalCard.Id} updated.");

        return _mapper.Map<GetMedicalCardDto>(medicalCard);
    }
}