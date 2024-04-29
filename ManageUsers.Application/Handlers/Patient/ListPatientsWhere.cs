using System.Linq.Expressions;

namespace ManageUsers.Application.Handlers.Patient;

internal static class ListPatientsWhere
{
    public static Expression<Func<Domain.Patient, bool>> Where(ListPatientFilter filter)
    {
        var freeText = filter.FreeText?.Trim();
        return user => freeText == null || user.LastName.Contains(freeText);
    }
}