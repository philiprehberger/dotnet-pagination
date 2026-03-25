using Xunit;
namespace Philiprehberger.Pagination.Tests;

public class CursorPagedResultTests
{
    [Fact]
    public void Constructor_SetsProperties()
    {
        var items = new[] { "a", "b", "c" };

        var result = new CursorPagedResult<string>(items, "cursor123", true);

        Assert.Equal(items, result.Items);
        Assert.Equal("cursor123", result.Cursor);
        Assert.True(result.HasMore);
    }

    [Fact]
    public void Constructor_NullCursor_IndicatesLastPage()
    {
        var items = new[] { 1, 2 };

        var result = new CursorPagedResult<int>(items, null, false);

        Assert.Null(result.Cursor);
        Assert.False(result.HasMore);
    }

    [Fact]
    public void Constructor_EmptyItems_IsValid()
    {
        var result = new CursorPagedResult<int>(Array.Empty<int>(), null, false);

        Assert.Empty(result.Items);
        Assert.Null(result.Cursor);
        Assert.False(result.HasMore);
    }
}
