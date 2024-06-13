using AutoFixture.Xunit2;

namespace Reviews.UnitTests.Core.Attributes;

public class FixtureInlineAutoDataAttribute : InlineAutoDataAttribute
{
    public FixtureInlineAutoDataAttribute(params object[] values) : base(new FixtureAutoDataAttribute(), values)
    {
    }
}