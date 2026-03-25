using Xunit;
namespace Philiprehberger.Pagination.Tests;

public class QueryableExtensionsTests
{
    private static IQueryable<int> CreateSource(int count) =>
        Enumerable.Range(1, count).AsQueryable();

    [Fact]
    public void ToPagedResult_FirstPage_ReturnsCorrectItems()
    {
        var source = CreateSource(25);

        var result = source.ToPagedResult(1, 10);

        Assert.Equal(10, result.Items.Count);
        Assert.Equal(25, result.TotalCount);
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
        Assert.True(result.HasNextPage);
    }

    [Fact]
    public void ToPagedResult_LastPage_ReturnsRemainingItems()
    {
        var source = CreateSource(25);

        var result = source.ToPagedResult(3, 10);

        Assert.Equal(5, result.Items.Count);
        Assert.Equal(25, result.TotalCount);
        Assert.False(result.HasNextPage);
    }

    [Fact]
    public void ToPagedResult_NullSource_ThrowsArgumentNullException()
    {
        IQueryable<int> source = null!;

        Assert.Throws<ArgumentNullException>(() => source.ToPagedResult(1, 10));
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(-1, 10)]
    public void ToPagedResult_InvalidPage_ThrowsArgumentOutOfRangeException(int page, int pageSize)
    {
        var source = CreateSource(10);

        Assert.Throws<ArgumentOutOfRangeException>(() => source.ToPagedResult(page, pageSize));
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(1, -1)]
    public void ToPagedResult_InvalidPageSize_ThrowsArgumentOutOfRangeException(int page, int pageSize)
    {
        var source = CreateSource(10);

        Assert.Throws<ArgumentOutOfRangeException>(() => source.ToPagedResult(page, pageSize));
    }

    [Fact]
    public async Task ToPagedResultAsync_ReturnsCorrectResult()
    {
        var source = CreateSource(15);

        var result = await source.ToPagedResultAsync(1, 10);

        Assert.Equal(10, result.Items.Count);
        Assert.Equal(15, result.TotalCount);
    }

    [Fact]
    public async Task ToPagedResultAsync_NullSource_ThrowsArgumentNullException()
    {
        IQueryable<int> source = null!;

        await Assert.ThrowsAsync<ArgumentNullException>(() => source.ToPagedResultAsync(1, 10));
    }
}
