using Contracts;
using MassTransit;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Persistence.Repository.Writing;
using MedicalCards.Application.Caches.MedicalCard;
using MedicalCards.Domain;
using Microsoft.Extensions.Logging;

namespace MedicalCards.Application.Features
{

    public class MedicalCardCreateConsumer : IConsumer<UserCreatedEvent>
    {


        private readonly IBaseWriteRepository<MedicalCard> _writeMedicalCardRepository;
        private readonly IBaseReadRepository<MedicalCard> _readMedicalCardsRepository;
        private readonly ILogger<MedicalCardCreateConsumer> _logger;
        private readonly MedicalCardsListMemoryCache _listCache;
        private readonly MedicalCardsCountMemoryCache _countCache;
        private readonly MedicalCardMemoryCache _medicalCardMemoryCache;
        public MedicalCardCreateConsumer(
           MedicalCardMemoryCache medicalCardMemoryCache,
            MedicalCardsListMemoryCache listCache,
            MedicalCardsCountMemoryCache countCache,
            ILogger<MedicalCardCreateConsumer> logger,
            IBaseReadRepository<MedicalCard> readMedicalCardsRepository,
            IBaseWriteRepository<MedicalCard> writeMedicalCardRepository)
        {

            _medicalCardMemoryCache = medicalCardMemoryCache;
            _listCache = listCache;
            _countCache = countCache;
            _logger = logger;
            _readMedicalCardsRepository = readMedicalCardsRepository;
            _writeMedicalCardRepository = writeMedicalCardRepository;
        }
        /// <summary>
        /// Create medical card after created patient
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {

            var medicalCard = await _readMedicalCardsRepository.AsAsyncRead().SingleOrDefaultAsync(e => e.PatientId == context.Message.Id, context.CancellationToken);
            if (medicalCard is not null)
            {
                return;
            }

            var newMedicalCard = MedicalCard.Create(
                  new Guid(),
                  context.Message.Id,
                  context.Message.FirstName,
                  context.Message.LastName,
                  context.Message.Patronymic,
                  context.Message.DateBirthday,
                  context.Message.PhoneNumber,
                  context.Message.Address
              );

            await _writeMedicalCardRepository.AddAsync(newMedicalCard, context.CancellationToken);
            _listCache.Clear();
            _countCache.Clear();
            _medicalCardMemoryCache.Clear();
            _logger.LogInformation($"New medicalCard {newMedicalCard.Id} was created.");
            _medicalCardMemoryCache.Clear();
        }
    }
}
