using AutoFixture.Xunit2;
using Reviews.UnitTests.Core.Helpers;

namespace Reviews.UnitTests.Core.Attributes;

public class FixtureAutoDataAttribute : AutoDataAttribute
{
    public FixtureAutoDataAttribute() : base(() => FixtureHelper.DefaultFixture)
    {
    }
}