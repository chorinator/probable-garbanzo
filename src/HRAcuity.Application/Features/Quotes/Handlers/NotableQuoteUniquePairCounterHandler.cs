using HRAcuity.Application.Features.Quotes.Queries;

namespace HRAcuity.Application.Features.Quotes.Handlers;

public class NotableQuoteUniquePairCounterHandler(
    IQueryHandlerAsync<NotableQuoteLengthQuery,
            IOrderedEnumerable<NotableQuoteLengthQuery.NotableQuotePairGroupsResult>>
        queryHandler)
    : IQueryHandlerAsync<NotableQuoteLengthQuery, NotableQuoteLengthQuery.NotableQuotePairsResult>
{
    public async Task<NotableQuoteLengthQuery.NotableQuotePairsResult>
        HandleAsync(NotableQuoteLengthQuery request, CancellationToken ct)
    {
        var orderedQuotes =
            await queryHandler.HandleAsync(request, ct);
        var asArray = orderedQuotes.ToArray();
        if (asArray.Length == 0)
            return new NotableQuoteLengthQuery.NotableQuotePairsResult(0);

        var pairs = 0L;

        // Formula provided by ChatGPT
        for (var i = 0; i < asArray.Length; i++)
        {
            for (var j = i; j < asArray.Length; j++)
            {
                if (asArray[i].Length + asArray[j].Length <= request.MaxLength)
                {
                    if (i == j)
                    {
                        // Combinations of 2 within the same group.
                        var quotes = asArray[i].Quotes;
                        pairs += quotes * (quotes - 1) / 2;
                    }
                    else
                        pairs += asArray[i].Quotes * asArray[j].Quotes;
                }
                else
                {
                    // Since groups are sorted, if we exceed maxLength here,
                    // further j's will also exceed, so we can break.
                    break;
                }
            }
        }

        return new NotableQuoteLengthQuery.NotableQuotePairsResult(pairs);
    }
}