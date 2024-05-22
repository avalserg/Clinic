using PatientTickets.Application.BaseRealizations;

namespace PatientTickets.Application;

public class PatientTicketsApplicationMappingRegister : MappingRegister
{
    public PatientTicketsApplicationMappingRegister() : base(typeof(PatientTicketsApplicationMappingRegister).Assembly)
    {
    }
}
