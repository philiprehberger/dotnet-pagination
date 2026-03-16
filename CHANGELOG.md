# Changelog

## 0.1.3

- Add Development section to README
- Add GenerateDocumentationFile, RepositoryType, PackageReadmeFile to .csproj

## 0.1.0 (2026-03-15)

- Initial release
- Offset-based `PagedResult<T>` with computed `TotalPages`, `HasNextPage`, `HasPreviousPage`
- Cursor-based `CursorPagedResult<T>` for cursor pagination
- `PaginationRequest` with configurable validation
- `IQueryable<T>` extensions: `ToPagedResult` and `ToPagedResultAsync`
