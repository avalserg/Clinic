using ManageUsers.Domain.Shared;

namespace ManageUsers.Domain.Errors;

public static class DomainErrors
{
    
    public static class PatientDomainErrors 
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            $"PatientDomainErrors.NotFound",
            $"The PatientDomainErrors with the identifier {id} was not found.");
    }
    public static class DoctorDomainErrors 
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            $"PatientDomainErrors.NotFound",
            $"The PatientDomainErrors with the identifier {id} was not found.");
    }
     public static class ApplicationUserDomainErrors
    {
        public static readonly Func<string, Error> LoginAlreadyInUse = login => new(
            "PatientDomainErrors.LoginAlreadyInUse",
            $"The patient login {login} is already in use");

        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "ApplicationUserDomainErrors.NotFound",
            $"The ApplicationUserDomainErrors with the identifier {id} was not found.");
    }

    public static class PhoneNumberDomainErrors
    {
        public static readonly Error Empty = new(
            "PhoneNumberDomainErrors.Empty",
            "PhoneNumberDomainErrors is empty");

      
        public static readonly Error InvalidFormat = new(
            "PhoneNumberDomainErrors.InvalidFormat",
            "PhoneNumberDomainErrors format is invalid");
    }

    public static class FirstNameDomainErrors
    {
        public static readonly Error Empty = new(
            "FirstNameDomainErrors.Empty",
            "First name is empty");

        public static readonly Error TooLong = new(
            "LastNameDomainErrors.TooLong",
            "FirstNameDomainErrors name is too long");
    }

    public static class LastNameDomainErrors
    {
        public static readonly Error Empty = new(
            "LastNameDomainErrors.Empty",
            "Last name is empty");

        public static readonly Error TooLong = new(
            "LastNameDomainErrors.TooLong",
            "Last name is too long");
    }
    public static class PatronymicDomainErrors
    {
        public static readonly Error Empty = new(
            "PatronymicDomainErrors.Empty",
            "PatronymicDomainErrors name is empty");

        public static readonly Error TooLong = new(
            "PatronymicDomainErrors.TooLong",
            "PatronymicDomainErrors name is too long");
    }
}
