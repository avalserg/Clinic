using ManageUsers.Application.DTOs;
using ManageUsers.Application.DTOs.Admin;
using MediatR;

namespace ManageUsers.Application.Handlers.Admin.Queries.GetAdmins
{
    public class GetAdminsQuery : ListAdminFilter, IBasePaginationFilter, IRequest<BaseListDto<GetAdminDto>>
    {
        public int? Limit { get; init; }
        public int? Offset { get; init; }
    }
}
