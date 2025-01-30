using HRAcuity.Application.Features.Quotes.Queries;

namespace HRAcuity.Persistence.Quotes;

public class DatabaseNotableQuoteUniquePairCounterHandler(IDbContextFactory<HrAcuityDbContext> dbContextFactory)
    : IQueryHandlerAsync<NotableQuoteLengthQuery, NotableQuoteLengthQuery.NotableQuotePairsResult>
{
    public async Task<IEnumerable<NotableQuoteLengthQuery.NotableQuotePairsResult>> HandleAsync(
        NotableQuoteLengthQuery request,
        CancellationToken ct)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(ct);
        var notableQuotes = dbContext.NotableQuotes;

        var totalCompliantPairs =
            (from q1 in notableQuotes
                from q2 in notableQuotes
                where q2.QuoteLength <= (request.MaxLength - q1.QuoteLength)
                      && q1.Id < q2.Id
                      && q1.QuoteHash != q2.QuoteHash
                select 1
            ).Count();

        var list = new List<NotableQuoteLengthQuery.NotableQuotePairsResult>
            { new(totalCompliantPairs) };
        return list;
    }
}