using HRAcuity.Application.Features.Quotes.Queries;

namespace HRAcuity.Application.Features.Quotes.Handlers;

public class NotableQuoteUniquePairCounterHandler(
    IQueryHandlerAsync<NotableQuoteLengthQuery, NotableQuoteLengthQuery.NotableQuoteLengthResult>
        queryHandler)
    : IQueryHandlerAsync<NotableQuoteLengthQuery, NotableQuoteLengthQuery.NotableQuotePairLengthResult>
{
    public async Task<IEnumerable<NotableQuoteLengthQuery.NotableQuotePairLengthResult>>
        HandleAsync(NotableQuoteLengthQuery request, CancellationToken ct)
    {
        var quotes = await queryHandler.HandleAsync(request, ct);
        var compliantPairs = new List<NotableQuoteLengthQuery.NotableQuotePairLengthResult>();
        var index = 0;

        foreach (var quote1 in quotes)
        {
            // Stop on last item
            if (index >= quotes.Count() - 1)
                break;

            foreach (var quote2 in quotes.Skip(index + 1))
            {
                ct.ThrowIfCancellationRequested();

                if (quote1.Length + quote2.Length > request.MaxLength)
                    continue;

                var combinedLength = quote1.Length + quote2.Length;
                var compliantQuotePair =
                    new NotableQuoteLengthQuery.NotableQuotePairLengthResult(
                        quote1.Quotes,
                        quote2.Quotes,
                        combinedLength);
                compliantPairs.Add(compliantQuotePair);
            }

            index++;
        }

        return compliantPairs.AsQueryable();
    }
}