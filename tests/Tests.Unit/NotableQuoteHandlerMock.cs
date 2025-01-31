using HRAcuity.Application.Contracts;
using HRAcuity.Application.Features.Quotes.Entities;
using HRAcuity.Application.Features.Quotes.Queries;
using Tests.Common;

namespace Tests.Unit;

internal class NotableQuoteHandlerMock(bool useLargeDb = false) :
    IQueryHandlerAsync<NotableQuoteLengthQuery,
        IOrderedEnumerable<NotableQuoteLengthQuery.NotableQuotePairGroupsResult>>
{
    private readonly NotableQuoteTestDataProvider _notableQuotesProvider = new(useLargeDb);

    public Task<IOrderedEnumerable<NotableQuoteLengthQuery.NotableQuotePairGroupsResult>> HandleAsync(
        NotableQuoteLengthQuery request, CancellationToken ct)
    {
        var result = _notableQuotesProvider
            .NotableQuotes
            .Where(nq => nq.Quote.Length <= request.MaxLength - NotableQuote.MinLength) // Shorter quote is Length 2
            .DistinctBy(nq => nq.Quote)
            .GroupBy(nq => nq.Quote.Length)
            .Select(nq =>
                new NotableQuoteLengthQuery.NotableQuotePairGroupsResult(
                    nq.Key, nq.Count()))
            .OrderBy(nq => nq.Length);

        return Task.FromResult(result);
    }
}