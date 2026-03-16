namespace Philiprehberger.Pagination;

/// <summary>
/// Represents a cursor-based paginated result set.
/// </summary>
/// <typeparam name="T">The type of items in the result.</typeparam>
/// <param name="Items">The items in the current page.</param>
/// <param name="Cursor">The cursor pointing to the next page, or <c>null</c> if there are no more pages.</param>
/// <param name="HasMore">A value indicating whether more items are available after this page.</param>
public record CursorPagedResult<T>(IReadOnlyList<T> Items, string? Cursor, bool HasMore);
