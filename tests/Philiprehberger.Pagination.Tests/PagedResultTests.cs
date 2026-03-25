using Xunit;
namespace Philiprehberger.Pagination.Tests;

public class PagedResultTests
{
    [Fact]
    public void TotalPages_CalculatesCorrectly()
    {
        var result = new PagedResult<int>(new[] { 1, 2, 3 }, 10, 1, 3);

        Assert.Equal(4, result.TotalPages);
    }

    [Fact]
    public void TotalPages_ZeroPageSize_ReturnsZero()
    {
        var result = new PagedResult<int>(Array.Empty<int>(), 0, 1, 0);

        Assert.Equal(0, result.TotalPages);
    }

    [Theory]
    [InlineData(1, 10, 5, true)]
    [InlineData(2, 10, 5, false)]
    [InlineData(1, 5, 5, false)]
    public void HasNextPage_ReturnsCorrectValue(int page, int totalCount, int pageSize, bool expected)
    {
        var result = new PagedResult<int>(Array.Empty<int>(), totalCount, page, pageSize);

        Assert.Equal(expected, result.HasNextPage);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(2, true)]
    [InlineData(5, true)]
    public void HasPreviousPage_ReturnsCorrectValue(int page, bool expected)
    {
        var result = new PagedResult<int>(Array.Empty<int>(), 100, page, 10);

        Assert.Equal(expected, result.HasPreviousPage);
    }

    [Fact]
    public void Empty_ReturnsEmptyResult()
    {
        var result = PagedResult<string>.Empty<string>();

        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
        Assert.Equal(1, result.Page);
        Assert.Equal(0, result.PageSize);
        Assert.False(result.HasNextPage);
        Assert.False(result.HasPreviousPage);
    }
}
