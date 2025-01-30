using HRAcuity.Persistence.Exceptions;

namespace HRAcuity.Persistence.Quotes;

public class UpdateNotableQuoteCommandHandler(
    IDbContextFactory<HrAcuityDbContext> dbContextFactory,
    IStringHasher stringHasher)
    : ICommandHandlerAsync<UpdateNotableQuoteCommand>
{
    public async Task HandleAsync(UpdateNotableQuoteCommand request, CancellationToken ct)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(ct);
        var entity = await dbContext.NotableQuotes.FindAsync([request.Id], cancellationToken: ct);

        if (entity is null)
            throw new NotFoundException("Notable quote not found.");

        entity.Quote = request.Quote;
        entity.Author = request.Author;
        entity.QuoteLength = request.Quote.Length;
        entity.QuoteHash = stringHasher.Hash(request.Quote);

        await dbContext.SaveChangesAsync(ct);
    }
}