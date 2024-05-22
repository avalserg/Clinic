using MediatR;

namespace ManageUsers.Application.Handlers.Admin.Queries.GetCountAdministrators
{
    public class GetCountAdministratorsQuery:ListAdminFilter,IRequest<int>
    {
    }
}
