using ManageUsers.Domain.Shared;

namespace ManageUsers.Domain.Errors;

public static class DomainErrors
{

    public static class PatientDomainErrors
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            $"PatientDomainErrors.NotFound",
            $"The PatientDomainErrors with the identifier {id} was not found.");
        public static readonly Func<string, Error> NotFoundStringId = id => new Error(
            $"PatientDomainErrors.NotFound",
            $"The PatientDomainErrors with the identifier {id} was not found.");
        public static readonly Func<string, Error> PassportNumberAlreadyInUse = passportNumber => new(
            "PatientDomainErrors.PassportNumberAlreadyInUse",
            $"The password number {passportNumber} is already in use");
        public static readonly Error PatientRoleNotFound = new(
            "PatientDomainErrors.PatientRoleNotFound",
            $"The role Patient is already in use");
    }
    public static class AdministratorDomainErrors
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            $"AdministratorDomainErrors.NotFound",
            $"The AdministratorDomainErrors with the identifier {id} was not found.");

    }
    public static class DoctorDomainErrors
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            $"DoctorDomainErrors.NotFound",
            $"The Doctor with the identifier {id} was not found.");
    }
    public static class ApplicationUserDomainErrors
    {
        public static readonly Func<string, Error> LoginAlreadyInUse = login => new(
            "ApplicationUserDomainErrors.LoginAlreadyInUse",
            $"The user with login {login} is already in use");

        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "ApplicationUserDomainErrors.NotFound",
            $"The user with the identifier {id} was not found.");
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
