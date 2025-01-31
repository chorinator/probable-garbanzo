namespace HRAcuity.Application.Features.Quotes.Queries;

public record GetQuoteByQuoteQuery(string Quote, int Page, int PageSize) :
    PaginatedQuery<NotableQuoteResults>(Page, PageSize);