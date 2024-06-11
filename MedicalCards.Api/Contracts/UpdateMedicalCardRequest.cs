namespace MedicalCards.Api.Contracts
{
    public sealed record UpdateMedicalCardRequest(
        string FirstName,
        string LastName,
        string Patronymic,
        string DateBirthday,
        string PhoneNumber,
        string Address
        );

}
