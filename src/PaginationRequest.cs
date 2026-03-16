namespace Philiprehberger.Pagination;

/// <summary>
/// Represents a pagination request with page number and page size.
/// </summary>
/// <param name="Page">The page number (1-based). Defaults to 1.</param>
/// <param name="PageSize">The number of items per page. Defaults to 20.</param>
public record PaginationRequest(int Page = 1, int PageSize = 20)
{
    /// <summary>
    /// The default maximum page size.
    /// </summary>
    public const int DefaultMaxPageSize = 100;

    /// <summary>
    /// Validates the pagination request and throws if invalid.
    /// </summary>
    /// <param name="maxPageSize">The maximum allowed page size. Defaults to <see cref="DefaultMaxPageSize"/>.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <see cref="Page"/> is less than 1 or <see cref="PageSize"/> is less than 1 or greater than <paramref name="maxPageSize"/>.
    /// </exception>
    public void Validate(int maxPageSize = DefaultMaxPageSize)
    {
        if (Page < 1)
            throw new ArgumentOutOfRangeException(nameof(Page), Page, "Page must be at least 1.");

        if (PageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(PageSize), PageSize, "PageSize must be at least 1.");

        if (PageSize > maxPageSize)
            throw new ArgumentOutOfRangeException(nameof(PageSize), PageSize, $"PageSize must not exceed {maxPageSize}.");
    }
}
