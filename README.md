# Philiprehberger.Pagination

Standardized pagination primitives — PagedResult, cursor-based pagination, and IQueryable extensions.

## Install

```bash
dotnet add package Philiprehberger.Pagination
```

## Usage

### Offset-Based Pagination

```csharp
using Philiprehberger.Pagination;

// Paginate a queryable source
var result = dbContext.Products
    .OrderBy(p => p.Name)
    .ToPagedResult(page: 1, pageSize: 20);

Console.WriteLine($"Page {result.Page} of {result.TotalPages}");
Console.WriteLine($"Showing {result.Items.Count} of {result.TotalCount} items");

if (result.HasNextPage)
    Console.WriteLine("More pages available");
```

### Cursor-Based Pagination

```csharp
using Philiprehberger.Pagination;

var cursorResult = new CursorPagedResult<Product>(
    Items: products,
    Cursor: lastProduct?.Id.ToString(),
    HasMore: products.Count == pageSize
);

if (cursorResult.HasMore)
    Console.WriteLine($"Next cursor: {cursorResult.Cursor}");
```

### Pagination Request Validation

```csharp
using Philiprehberger.Pagination;

var request = new PaginationRequest(Page: 1, PageSize: 50);
request.Validate(); // throws if invalid (page < 1, pageSize < 1 or > 100)

// Custom max page size
request.Validate(maxPageSize: 200);
```

### Async Pagination

```csharp
using Philiprehberger.Pagination;

var result = await queryable.ToPagedResultAsync(page: 2, pageSize: 10, cancellationToken);
```

> **Note:** The async extension uses `Task.Run` internally. If you are using EF Core, prefer calling `CountAsync` and `ToListAsync` directly for true async database operations.

## API

### `PagedResult<T>`

| Member | Type | Description |
|--------|------|-------------|
| `Items` | `IReadOnlyList<T>` | Items on the current page |
| `TotalCount` | `int` | Total number of items across all pages |
| `Page` | `int` | Current page number (1-based) |
| `PageSize` | `int` | Maximum items per page |
| `TotalPages` | `int` | Computed total number of pages |
| `HasNextPage` | `bool` | Whether a next page exists |
| `HasPreviousPage` | `bool` | Whether a previous page exists |
| `Empty<T>()` | `PagedResult<T>` | Creates an empty result |

### `CursorPagedResult<T>`

| Member | Type | Description |
|--------|------|-------------|
| `Items` | `IReadOnlyList<T>` | Items in the current page |
| `Cursor` | `string?` | Cursor for the next page, or null |
| `HasMore` | `bool` | Whether more items are available |

### `PaginationRequest`

| Member | Type | Description |
|--------|------|-------------|
| `Page` | `int` | Page number (default: 1) |
| `PageSize` | `int` | Items per page (default: 20) |
| `Validate(maxPageSize)` | `void` | Validates the request, throws on invalid values |

### `QueryableExtensions`

| Method | Returns | Description |
|--------|---------|-------------|
| `ToPagedResult<T>(page, pageSize)` | `PagedResult<T>` | Paginates a queryable source |
| `ToPagedResultAsync<T>(page, pageSize, ct)` | `Task<PagedResult<T>>` | Async pagination via Task.Run |

## License

MIT
