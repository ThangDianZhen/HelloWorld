using Ignite.Application.Helpers;

namespace Ignite.Application.Tests;

public class SlugHelperTests
{
    [Theory]
    [InlineData("Ignite Kuala Lumpur", "ignite-kuala-lumpur")]
    [InlineData("  Night of Worship!  ", "night-of-worship")]
    [InlineData("Kids & Youth", "kids-youth")]
    public void ToSlug_NormalizesTitles(string input, string expected)
    {
        Assert.Equal(expected, SlugHelper.ToSlug(input));
    }

    [Fact]
    public void ToSlug_Empty_ReturnsEmpty()
    {
        Assert.Equal(string.Empty, SlugHelper.ToSlug("   "));
    }
}
