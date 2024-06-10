namespace PatientTickets.ExternalProviders.Exceptions
{
    public class ExternalServiceNotAvailable : Exception
    {
        public ExternalServiceNotAvailable(string serviceName, string message) : base($"serviceName: {serviceName}, message: {message}")
        {

        }
    }
}