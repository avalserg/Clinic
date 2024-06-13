namespace Reviews.UnitTests.Core.Helpers;

public static class GuidHelper
{
    public static Guid GetNotEqualGiud(Guid guid)
    {
        var newGuid = guid;
        while (newGuid == guid)
        {
            newGuid = Guid.NewGuid();
        }

        return newGuid;
    }
}