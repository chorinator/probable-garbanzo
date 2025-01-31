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
        var pairs = 0L;

        foreach (var (lenght, quotes) in asArray)
        {
            var compliantGroups =
                asArray
                    .Where(nq =>
                        lenght + nq.Length <= request.MaxLength)
                    .ToArray();

            if (compliantGroups.Length == 0)
                break;

            var totalCompliantPairs =
                compliantGroups
                    .Sum(nq => nq.Quotes);

            // Formula provided by ChatGPT
            pairs += totalCompliantPairs * (totalCompliantPairs - 1) / 2;
        }

        return new NotableQuoteLengthQuery.NotableQuotePairsResult(pairs);
    }
}