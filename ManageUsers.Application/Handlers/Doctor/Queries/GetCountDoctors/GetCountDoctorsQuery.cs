using MediatR;

namespace ManageUsers.Application.Handlers.Doctor.Queries.GetCountDoctors
{
    public class GetCountDoctorsQuery:ListDoctorsFilter,IRequest<int>
    {
    }
}
