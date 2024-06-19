using System.Linq.Expressions;

namespace ManageUsers.Application.Handlers.Doctor;

internal static class ListDoctorsWhere
{
    public static Expression<Func<Domain.Doctor, bool>> Where(ListDoctorsFilter filter)
    {
        var freeText = filter.FreeText?.Trim();
        return user => freeText == null || user.Category.Contains(freeText);
    }
}