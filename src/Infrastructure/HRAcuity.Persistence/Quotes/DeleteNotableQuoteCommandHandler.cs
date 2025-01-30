using HRAcuity.Persistence.Exceptions;

namespace HRAcuity.Persistence.Quotes;

public class DeleteNotableQuoteCommandHandler(
    IDbContextFactory<HrAcuityDbContext> dbContextFactory)
    : ICommandHandlerAsync<DeleteNotableQuoteCommand>
{
    public async Task HandleAsync(DeleteNotableQuoteCommand request, CancellationToken ct)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(ct);
        var entity = await dbContext.NotableQuotes.FindAsync([request.Id], cancellationToken: ct);
        
        if (entity is null)
            throw new NotFoundException("Notable quote not found.");

        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync(ct);
    }
}