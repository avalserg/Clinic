namespace PatientTickets.Application.DTOs.ExternalProviders;

public class GetDoctorDto
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }

    public string CabinetNumber { get; set; }
    public string Speciality { get; set; }
    public GetDoctorDto(string firstName, string lastName, string patronymic, string cabinetNumber, string speciality)
    {

        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        CabinetNumber = cabinetNumber;
        Speciality = speciality;
    }
    public GetDoctorDto() { }
}
