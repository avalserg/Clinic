using Contracts;
using MassTransit;
using MedicalCards.Application.Abstractions.Persistence.Repository.Writing;
using MedicalCards.Application.Caches.MedicalCard;
using MedicalCards.Domain;
using Microsoft.Extensions.Logging;

namespace MedicalCards.Application.Features
{
    public sealed class MedicalCardOwnerUpdatedConsumer : IConsumer<UserUpdatedEvent>
    {
        private readonly MedicalCardsListMemoryCache _listCache;
        private readonly ILogger<MedicalCardOwnerUpdatedConsumer> _logger;
        private readonly MedicalCardMemoryCache _medicalCardMemoryCache;
        private readonly IBaseWriteRepository<MedicalCard> _medicalCards;
        /// <summary>
        /// Update medical card after updated patient
        /// </summary>
        /// <param name="medicalCards"></param>
        /// <param name="medicalCardMemoryCache"></param>
        /// <param name="listCache"></param>
        /// <param name="logger"></param>
        public MedicalCardOwnerUpdatedConsumer(
            IBaseWriteRepository<MedicalCard> medicalCards,
            MedicalCardMemoryCache medicalCardMemoryCache,
            MedicalCardsListMemoryCache listCache,
            ILogger<MedicalCardOwnerUpdatedConsumer> logger)
        {
            _medicalCards = medicalCards;
            _medicalCardMemoryCache = medicalCardMemoryCache;
            _listCache = listCache;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<UserUpdatedEvent> context)
        {

            var medicalCard = await _medicalCards.AsAsyncRead().SingleOrDefaultAsync(e => e.PatientId == context.Message.Id, context.CancellationToken);
            if (medicalCard is null)
            {
                return;
            }

            medicalCard.UpdateOwnerMedicalCardInfo(
                context.Message.FirstName,
                context.Message.LastName,
                context.Message.Patronymic,
                context.Message.DateBirthday,
                context.Message.PhoneNumber,
                context.Message.Address
            );

            await _medicalCards.UpdateAsync(medicalCard, context.CancellationToken);
            _listCache.Clear();
            _medicalCardMemoryCache.Clear();
            _logger.LogInformation($"Medical Card {medicalCard.Id} updated.");
        }
    }
}
