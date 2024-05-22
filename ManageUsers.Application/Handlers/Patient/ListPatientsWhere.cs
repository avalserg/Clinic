using System.Linq.Expressions;

namespace ManageUsers.Application.Handlers.Patient;

internal static class ListAdminWhere
{
    public static Expression<Func<Domain.Patient, bool>> Where(ListPatientFilter filter)
    {
        var freeText = filter.FreeText?.Trim();
        return user => freeText == null || user.FullName.LastName.Contains(freeText);
    }
}