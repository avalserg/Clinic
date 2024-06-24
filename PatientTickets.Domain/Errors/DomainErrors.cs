using PatientTickets.Domain.Shared;

namespace PatientTickets.Domain.Errors;

public static class DomainErrors
{

    public static class PatientTicket
    {
        public static readonly Func<string, Error> PatientTicketTimeIsBusy = time => new(
              "PatientTicket.PatientTicketTimeIsBusy",
              $"The time {time} is already in use");
        public static readonly Func<string, Error> PatientTicketTimeAlreadyPassed = time => new(
              "PatientTicket.PatientTicketTimeAlreadyPassed",
              $"The time {time} is already passed");
        public static readonly Func<Guid, Error> PatientTicketNotFound = id => new(
              "PatientTicket.PatientTicketNotFound",
              $"The PatientTicket {id} not found");
        public static readonly Func<Guid, Error> DoctorForPatientTicketNotFound = id => new(
              "PatientTicket.DoctorForPatientTicketNotFound",
              $"The Doctor with {id} not found");
        public static readonly Func<Guid, Error> CreatorIsNotInRolePatient = id => new(
              "PatientTicket.CreatorIsNotInRolePatient",
              $"The user with {id} not have patient role");


    }

}
