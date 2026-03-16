namespace Philiprehberger.Pagination;

/// <summary>
/// Extension methods for <see cref="IQueryable{T}"/> to support pagination.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Paginates a queryable source and returns a <see cref="PagedResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <param name="source">The queryable source to paginate.</param>
    /// <param name="page">The page number (1-based).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A <see cref="PagedResult{T}"/> containing the paginated items.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="page"/> or <paramref name="pageSize"/> is less than 1.</exception>
    public static PagedResult<T> ToPagedResult<T>(this IQueryable<T> source, int page, int pageSize)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page), page, "Page must be at least 1.");
        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "PageSize must be at least 1.");

        var totalCount = source.Count();
        var items = source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResult<T>(items, totalCount, page, pageSize);
    }

    /// <summary>
    /// Asynchronously paginates a queryable source and returns a <see cref="PagedResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <param name="source">The queryable source to paginate.</param>
    /// <param name="page">The page number (1-based).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    /// <returns>A task that resolves to a <see cref="PagedResult{T}"/> containing the paginated items.</returns>
    /// <remarks>
    /// This method uses <see cref="Task.Run{TResult}(Func{TResult}, CancellationToken)"/> for async execution
    /// since there is no dependency on EF Core. If you are using EF Core, prefer using
    /// <c>CountAsync</c> and <c>ToListAsync</c> directly for true async database operations.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="page"/> or <paramref name="pageSize"/> is less than 1.</exception>
    public static Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> source,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page), page, "Page must be at least 1.");
        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "PageSize must be at least 1.");

        return Task.Run(() => source.ToPagedResult(page, pageSize), cancellationToken);
    }
}
