namespace ManageUsers.Api.Contracts.Admin
{
    public sealed record UpdateAdministratorRequest(
            string FirstName,
            string LastName,
            string Patronymic
        );
}
