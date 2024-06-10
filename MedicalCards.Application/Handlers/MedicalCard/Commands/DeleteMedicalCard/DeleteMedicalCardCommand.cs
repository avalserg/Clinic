using MedicalCards.Application.Abstractions.Messaging;

namespace MedicalCards.Application.Handlers.MedicalCard.Commands.DeleteMedicalCard;

//[RequestAuthorize]
public class DeleteMedicalCardCommand : ICommand
{
    public Guid Id { get; init; } = default!;
}