namespace HRAcuity.Application.Features.Quotes.Queries;

public record NotableQuoteLengthQuery(int MaxLength)
    : IQuery<IOrderedEnumerable<NotableQuoteLengthQuery.NotableQuoteLengthResult>>,
        IQuery<NotableQuoteLengthQuery.NotableQuotePairsResult>,
        IQuery<NotableQuoteLengthQuery.NotableQuotePairLengthResult>,
        IQuery<IOrderedEnumerable<NotableQuoteLengthQuery.NotableQuotePairGroupsResult>>,
        IQuery<NotableQuoteLengthQuery.SlimNotableQuoteLengthResult>
{
    public record NotableQuoteLengthResult(NotableQuote Quotes, int Length);

    public record SlimNotableQuoteLengthResult(int Quote1Id, int Quote2Id, int CombinedLength);

    public record NotableQuotePairsResult(long Matches);
    public record NotableQuotePairGroupsResult(int Length, long Quotes);

    public record NotableQuotePairLengthResult(NotableQuote Quote1, NotableQuote Quote2, int CombinedLength);
}