namespace MedicalCards.Application.DTOs.ExternalProviders;

public class GetDoctorDto
{

    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorPatronymic { get; set; }


    public string Speciality { get; set; }
    public GetDoctorDto(string doctorFirstName, string doctorLastName, string doctorPatronymic, string speciality)
    {

        DoctorFirstName = doctorFirstName;
        DoctorLastName = doctorLastName;
        DoctorPatronymic = doctorPatronymic;

        Speciality = speciality;
    }
    public GetDoctorDto() { }
}
