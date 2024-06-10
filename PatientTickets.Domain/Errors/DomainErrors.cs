using PatientTickets.Domain.Shared;

namespace PatientTickets.Domain.Errors;

public static class DomainErrors
{

    public static class PatientTicket
    {
        public static readonly Func<string, Error> PatientTicketTimeIsBusy = time => new(
              "PatientTicket.PatientTicketTimeIsBusy",
              $"The login {time} is already in use");
        public static readonly Func<Guid, Error> PatientTicketNotFound = id => new(
              "PatientTicket.PatientTicketNotFound",
              $"The PatientTicket {id} not found");


    }

}
