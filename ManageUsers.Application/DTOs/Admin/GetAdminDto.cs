using ManageUsers.Application.Abstractions.Mappings;
using ManageUsers.Domain;
using ManageUsers.Domain.ValueObjects;

namespace ManageUsers.Application.DTOs.Admin
{
    public class GetAdminDto:IMapFrom<Administrator>
    {
        public Guid Id { get; set; }
        public FullName FullName { get; set; }
        public Guid ApplicationUserId { get; set; }
    }
}
