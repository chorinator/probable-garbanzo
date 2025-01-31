namespace HRAcuity.Application.Features.Quotes.Queries;

public record GetQuoteByAuthorQuery(string Author, int Page, int PageSize) :
    PaginatedQuery<NotableQuoteResults>(Page, PageSize);