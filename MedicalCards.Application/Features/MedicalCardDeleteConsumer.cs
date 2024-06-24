using Contracts;
using MassTransit;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Persistence.Repository.Writing;
using MedicalCards.Application.Caches.MedicalCard;
using MedicalCards.Domain;
using Microsoft.Extensions.Logging;

namespace MedicalCards.Application.Features
{
    internal class MedicalCardDeleteConsumer : IConsumer<UserDeletedEvent>
    {
        private readonly IBaseWriteRepository<MedicalCard> _writeMedicalCardRepository;
        private readonly IBaseReadRepository<MedicalCard> _readMedicalCardsRepository;
        private readonly ILogger<MedicalCardDeleteConsumer> _logger;
        private readonly MedicalCardsListMemoryCache _listCache;
        private readonly MedicalCardsCountMemoryCache _countCache;
        private readonly MedicalCardMemoryCache _medicalCardMemoryCache;
        public MedicalCardDeleteConsumer(IBaseWriteRepository<MedicalCard> writeMedicalCardRepository, IBaseReadRepository<MedicalCard> readMedicalCardsRepository, ILogger<MedicalCardDeleteConsumer> logger, MedicalCardsListMemoryCache listCache, MedicalCardsCountMemoryCache countCache, MedicalCardMemoryCache medicalCardMemoryCache)
        {
            _writeMedicalCardRepository = writeMedicalCardRepository;
            _readMedicalCardsRepository = readMedicalCardsRepository;
            _logger = logger;
            _listCache = listCache;
            _countCache = countCache;
            _medicalCardMemoryCache = medicalCardMemoryCache;
        }
        public async Task Consume(ConsumeContext<UserDeletedEvent> context)
        {
            var medicalCard = await _readMedicalCardsRepository.AsAsyncRead().SingleOrDefaultAsync(e => e.PatientId == context.Message.Id, context.CancellationToken);
            if (medicalCard is null)
            {
                return;
            }
            medicalCard.UpdateIsDeletedToTrue();
            await _writeMedicalCardRepository.UpdateAsync(medicalCard, context.CancellationToken);
            _listCache.Clear();
            _countCache.Clear();
            _medicalCardMemoryCache.Clear();
            _logger.LogInformation($"MedicalCard {medicalCard.Id} was deleted.");
            _medicalCardMemoryCache.Clear();
        }
    }
}
