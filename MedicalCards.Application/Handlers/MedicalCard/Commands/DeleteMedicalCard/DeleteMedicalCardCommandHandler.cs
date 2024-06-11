using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.Abstractions.Persistence.Repository.Writing;
using MedicalCards.Application.Abstractions.Service;
using MedicalCards.Application.Caches.MedicalCard;
using MedicalCards.Application.Handlers.MedicalCard.Queries.GetMedicalCard;
using MedicalCards.Domain.Enums;
using MedicalCards.Domain.Errors;
using MedicalCards.Domain.Exceptions.Base;
using MedicalCards.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace MedicalCards.Application.Handlers.MedicalCard.Commands.DeleteMedicalCard;

internal class DeleteMedicalCardCommandHandler : ICommandHandler<DeleteMedicalCardCommand>
{
    private readonly IBaseWriteRepository<Domain.MedicalCard> _medicalCards;

    private readonly ICurrentUserService _currentUserService;

    private readonly MedicalCardsListMemoryCache _listCache;

    private readonly MedicalCardsCountMemoryCache _countCache;

    private readonly ILogger<DeleteMedicalCardCommandHandler> _logger;

    private readonly MedicalCardMemoryCache _medicalCardCache;

    public DeleteMedicalCardCommandHandler(
        IBaseWriteRepository<Domain.MedicalCard> medicalCards,
        ICurrentUserService currentUserService,
        MedicalCardsListMemoryCache listCache,
        MedicalCardsCountMemoryCache countCache,
        ILogger<DeleteMedicalCardCommandHandler> logger,
        MedicalCardMemoryCache medicalCardCache)
    {
        _medicalCards = medicalCards;
        _currentUserService = currentUserService;
        _listCache = listCache;
        _countCache = countCache;
        _logger = logger;
        _medicalCardCache = medicalCardCache;
    }
    // TODO check as removed
    public async Task<Result> Handle(DeleteMedicalCardCommand request, CancellationToken cancellationToken)
    {

        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        var medicalCard = await _medicalCards.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (medicalCard is null)
        {
            return Result.Failure(
                DomainErrors.MedicalCard.MedicalCardNotFound(request.Id));
        }
        await _medicalCards.RemoveAsync(medicalCard, cancellationToken);


        _listCache.Clear();
        _countCache.Clear();
        _logger.LogWarning(
            $"User {medicalCard.Id} deleted by {_currentUserService.CurrentUserId}");
        _medicalCardCache.DeleteItem(new GetMedicalCardQuery() { Id = medicalCard.Id });
        return Result.Success();
    }


}