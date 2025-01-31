namespace HRAcuity.Application.Features.Quotes.Queries;

public record NotableQuoteResults(
    int Page,
    int PageSize,
    int TotalPages,
    int TotalCount,
    IEnumerable<NotableQuote> Quotes)
    : PaginatedResult<NotableQuote>(Page, PageSize, TotalPages, TotalCount, Quotes);