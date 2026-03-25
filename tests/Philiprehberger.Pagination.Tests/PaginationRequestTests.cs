using Xunit;
namespace Philiprehberger.Pagination.Tests;

public class PaginationRequestTests
{
    [Fact]
    public void Defaults_Page1_PageSize20()
    {
        var request = new PaginationRequest();

        Assert.Equal(1, request.Page);
        Assert.Equal(20, request.PageSize);
    }

    [Fact]
    public void Validate_ValidRequest_DoesNotThrow()
    {
        var request = new PaginationRequest(1, 50);

        var exception = Record.Exception(() => request.Validate());

        Assert.Null(exception);
    }

    [Fact]
    public void Validate_PageLessThanOne_ThrowsArgumentOutOfRangeException()
    {
        var request = new PaginationRequest(0, 20);

        Assert.Throws<ArgumentOutOfRangeException>(() => request.Validate());
    }

    [Fact]
    public void Validate_PageSizeLessThanOne_ThrowsArgumentOutOfRangeException()
    {
        var request = new PaginationRequest(1, 0);

        Assert.Throws<ArgumentOutOfRangeException>(() => request.Validate());
    }

    [Fact]
    public void Validate_PageSizeExceedsMax_ThrowsArgumentOutOfRangeException()
    {
        var request = new PaginationRequest(1, 101);

        Assert.Throws<ArgumentOutOfRangeException>(() => request.Validate());
    }

    [Fact]
    public void Validate_CustomMaxPageSize_RespectsLimit()
    {
        var request = new PaginationRequest(1, 30);

        Assert.Throws<ArgumentOutOfRangeException>(() => request.Validate(maxPageSize: 25));
    }

    [Fact]
    public void DefaultMaxPageSize_Is100()
    {
        Assert.Equal(100, PaginationRequest.DefaultMaxPageSize);
    }
}
