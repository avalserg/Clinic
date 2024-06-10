namespace Reviews.Application.DTOs.ExternalProviders;

public class GetPatientDto
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }

    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateBirthday { get; set; }
    public GetPatientDto(string firstName, string lastName, string patronymic, DateTime dateBirthday, string phoneNumber, string address)
    {

        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        DateBirthday = dateBirthday;
        PhoneNumber = phoneNumber;
        Address = address;
    }
    public GetPatientDto() { }
}
