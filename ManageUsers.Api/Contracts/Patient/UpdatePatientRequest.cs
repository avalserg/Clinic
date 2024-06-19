namespace ManageUsers.Api.Contracts.Patient
{
    public sealed record UpdatePatientRequest(
        string Address,
        string? Avatar,
        string DateBirthday,
        string FirstName,
        string LastName,
        string Patronymic,
        string PhoneNumber,
        string PassportNumber
        );




}
