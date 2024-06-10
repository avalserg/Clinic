using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.DTOs.MedicalCard;

namespace MedicalCards.Application.Handlers.MedicalCard.Commands.UpdateMedicalCard;


public sealed record UpdateMedicalCardCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Patronymic,
    DateTime DateBirthday,
    string Address,
    string PhoneNumber
    ) : ICommand<GetMedicalCardDto>;
