using ManageUsers.Api.Contracts.Shared;

namespace ManageUsers.Api.Contracts.Patient
{
    public sealed record UpdatePatientRequest(
        string Address,
        Guid ApplicationUserId,
        string? Avatar,
        string DateBirthday,
        string FirstName,
        string LastName,
        string Patronymic,
        PhoneNumber PhoneNumber,
        string PassportNumber
        );
     
  
   

}
