using PatientTickets.Domain.Shared;

namespace PatientTickets.Domain.Errors;

public static class DomainErrors
{
    
    public static class Patient 
    {
        public static readonly Func<string, Error> LoginAlreadyInUse =login=> new(
              "Patient.LoginAlreadyInUse",
              $"The login {login} is already in use");

        public static readonly Func<Guid, Error> NotFound = id => new Error(
            $"Patient.NotFound",
            $"The Patient with the identifier {id} was not found.");
    }
     public static class ApplicationUser
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "ApplicationUser.NotFound",
            $"The ApplicationUser with the identifier {id} was not found.");
    }

    public static class PhoneNumber
    {
        public static readonly Error Empty = new(
            "PhoneNumber.Empty",
            "PhoneNumber is empty");

      
        public static readonly Error InvalidFormat = new(
            "PhoneNumber.InvalidFormat",
            "PhoneNumber format is invalid");
    }

    public static class FirstName
    {
        public static readonly Error Empty = new(
            "FirstName.Empty",
            "First name is empty");

        public static readonly Error TooLong = new(
            "LastName.TooLong",
            "FirstName name is too long");
    }

    public static class LastName
    {
        public static readonly Error Empty = new(
            "LastName.Empty",
            "Last name is empty");

        public static readonly Error TooLong = new(
            "LastName.TooLong",
            "Last name is too long");
    }
    public static class Patronymic
    {
        public static readonly Error Empty = new(
            "Patronymic.Empty",
            "Patronymic name is empty");

        public static readonly Error TooLong = new(
            "Patronymic.TooLong",
            "Patronymic name is too long");
    }
}
