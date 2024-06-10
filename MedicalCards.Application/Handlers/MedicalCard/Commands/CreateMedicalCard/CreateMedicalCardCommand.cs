using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.DTOs.MedicalCard;

namespace MedicalCards.Application.Handlers.MedicalCard.Commands.CreateMedicalCard;

public class CreateMedicalCardCommand : ICommand<CreateMedicalCardDto>
{
    public Guid PatientId { get; init; }


}

