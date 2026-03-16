namespace Philiprehberger.Pagination;

/// <summary>
/// Represents an offset-based paginated result set.
/// </summary>
/// <typeparam name="T">The type of items in the result.</typeparam>
/// <param name="Items">The items on the current page.</param>
/// <param name="TotalCount">The total number of items across all pages.</param>
/// <param name="Page">The current page number (1-based).</param>
/// <param name="PageSize">The maximum number of items per page.</param>
public record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount, int Page, int PageSize)
{
    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;

    /// <summary>
    /// Gets a value indicating whether there is a next page.
    /// </summary>
    public bool HasNextPage => Page < TotalPages;

    /// <summary>
    /// Gets a value indicating whether there is a previous page.
    /// </summary>
    public bool HasPreviousPage => Page > 1;

    /// <summary>
    /// Creates an empty paged result with no items.
    /// </summary>
    /// <typeparam name="TItem">The type of items.</typeparam>
    /// <returns>An empty <see cref="PagedResult{T}"/> with zero total count.</returns>
    public static PagedResult<TItem> Empty<TItem>() =>
        new(Array.Empty<TItem>(), 0, 1, 0);
}
