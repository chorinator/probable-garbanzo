using HRAcuity.Application.Features.Quotes.Entities;
using HRAcuity.Application.Features.Quotes.Queries;

namespace HRAcuity.Persistence.Quotes;

public class NotableQuoteUniquePairCounterHandler(IDbContextFactory<HrAcuityDbContext> dbContextFactory)
    : IQueryHandlerAsync<NotableQuoteLengthQuery, NotableQuoteLengthQuery.NotableQuoteLengthResult>
{
    public async Task<IEnumerable<NotableQuoteLengthQuery.NotableQuoteLengthResult>> HandleAsync(
        NotableQuoteLengthQuery request,
        CancellationToken ct)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(ct);

        var quotes = await
            dbContext.NotableQuotes
                .Where(nq =>
                    nq.QuoteLength <= request.MaxLength)
                .OrderBy(nq => nq.Id)
                .Select(nq =>
                    new NotableQuoteLengthQuery.NotableQuoteLengthResult(
                        new NotableQuote(nq.Id, nq.Author, nq.Quote),
                        nq.QuoteLength))
                .ToListAsync(ct);

        return quotes;
    }
}