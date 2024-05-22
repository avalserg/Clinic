using System.Linq.Expressions;
using ManageUsers.Domain;

namespace ManageUsers.Application.Handlers.Admin;

internal static class ListAdminWhere
{
    public static Expression<Func<Administrator, bool>> Where(ListAdminFilter filter)
    {
        var freeText = filter.FreeText?.Trim();
        return user => freeText == null || user.FullName.LastName.Contains(freeText);
    }
}