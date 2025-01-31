namespace HRAcuity.Application.Features.Quotes.Entities;

public record PaginatedResult(
    int Page,
    int PageSize,
    int TotalPages,
    int TotalCount
);

public record PaginatedResult<T>(
    int Page,
    int PageSize,
    int TotalPages,
    int TotalCount,
    IEnumerable<T> Items) :
    PaginatedResult(Page, PageSize, TotalPages, TotalCount)
    where T : class;