using HRAcuity.Persistence.Entities;

namespace HRAcuity.Persistence.Quotes;

public class InsertNotableQuoteCommandHandler(
    IDbContextFactory<HrAcuityDbContext> dbContextFactory,
    IStringHasher stringHasher)
    : ICommandHandlerAsync<InsertNotableQuotesCommand>
{
    public async Task HandleAsync(InsertNotableQuotesCommand request, CancellationToken ct)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(ct);
        var notableQuotes =
            request
                .Select(nq =>
                    new NotableQuoteEntity
                    {
                        // Id is generated by the database
                        Quote = nq.Quote,
                        QuoteLength = nq.Quote.Length,
                        Author = nq.Author,
                        QuoteHash = stringHasher.Hash(nq.Quote)
                    });

        await dbContext.NotableQuotes.AddRangeAsync(notableQuotes, ct);
        await dbContext.SaveChangesAsync(ct);
    }
}