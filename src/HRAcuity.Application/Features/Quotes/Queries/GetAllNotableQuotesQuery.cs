namespace HRAcuity.Application.Features.Quotes.Queries;

public record GetAllNotableQuotesQuery(int Page, int PageSize) : IQuery<IEnumerable<NotableQuote>>;