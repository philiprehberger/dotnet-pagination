# Changelog

## 0.1.7 (2026-03-24)

- Add unit tests
- Add test step to CI workflow

## 0.1.6 (2026-03-24)

- Sync .csproj description with README

## 0.1.5 (2026-03-22)

- Add dates to changelog entries

## 0.1.4 (2026-03-17)

- Rename Install section to Installation in README per package guide

## 0.1.3 (2026-03-16)

- Add Development section to README
- Add GenerateDocumentationFile, RepositoryType, PackageReadmeFile to .csproj

## 0.1.0 (2026-03-15)

- Initial release
- Offset-based `PagedResult<T>` with computed `TotalPages`, `HasNextPage`, `HasPreviousPage`
- Cursor-based `CursorPagedResult<T>` for cursor pagination
- `PaginationRequest` with configurable validation
- `IQueryable<T>` extensions: `ToPagedResult` and `ToPagedResultAsync`
