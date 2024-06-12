namespace ManageUsers.Api.Contracts.Doctor
{
    public sealed record UpdateDoctorRequest(
        string Address,
        Guid ApplicationUserId,
        string DateBirthday,
        string FirstName,
        string LastName,
        string Patronymic,
        string PhoneNumber,
        string CabinetNumber,
        int Experience,
        string Category,
        string Speciality
        );




}
