namespace ManageUsers.Api.Contracts.Patient
{
    public sealed record CreatePatientRequest(

    string Address,
    string DateBirthday,
    string FirstName,
    string LastName,
    string Login,
    string Password,
    string Patronymic,
    string PhoneNumber,
    string PassportNumber,
    string? Avatar);
}
