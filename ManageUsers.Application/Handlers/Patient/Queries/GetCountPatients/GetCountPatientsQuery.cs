using MediatR;

namespace ManageUsers.Application.Handlers.Patient.Queries.GetCountPatients
{
    public class GetCountPatientsQuery:ListPatientFilter,IRequest<int>
    {
    }
}
