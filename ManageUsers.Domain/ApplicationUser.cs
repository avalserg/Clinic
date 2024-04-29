using ManageUsers.Domain.Enums;
using ManageUsers.Domain.Primitives;

namespace ManageUsers.Domain;

public class ApplicationUser:Entity
{
    public Guid ApplicationUserId { get; set; } = default!;

    public string Login { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;
    public ApplicationUserRoles ApplicationUserRole { get; set; } = default!;

    //public DateTime CreatedDate { get; set; }

    //public DateTime? UpdatedDate { get; set; }

    //public DateTime? LastSingInDate { get; set; }


}