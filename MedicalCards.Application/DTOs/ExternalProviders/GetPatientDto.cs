namespace MedicalCards.Application.DTOs.ExternalProviders;

public class GetPatientDto
{

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Patronymic { get; set; } = default!;

    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
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
