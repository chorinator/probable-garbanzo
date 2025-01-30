using HRAcuity.Application.Contracts;
using HRAcuity.Application.Features.Quotes.Queries;
using Tests.Common;

namespace Tests.Unit;

internal class NotableQuoteHandlerMock(bool useLargeDb = false) :
    IQueryHandlerAsync<NotableQuoteLengthQuery, NotableQuoteLengthQuery.NotableQuoteLengthResult>
{
    private readonly NotableQuoteTestDataProvider _notableQuotesProvider = new(useLargeDb);

    public Task<IEnumerable<NotableQuoteLengthQuery.NotableQuoteLengthResult>> HandleAsync(
        NotableQuoteLengthQuery request, CancellationToken ct)
    {
        var result = _notableQuotesProvider
            .NotableQuotes
            .DistinctBy(nq => nq.Id)
            .Select(nq =>
                new NotableQuoteLengthQuery.NotableQuoteLengthResult(nq, nq.Quote.Length));

        return Task.FromResult(result);
    }
}