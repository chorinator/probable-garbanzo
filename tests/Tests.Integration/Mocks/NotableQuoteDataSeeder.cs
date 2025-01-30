using HRAcuity.Application.Contracts;
using HRAcuity.Application.Features.Quotes.Commands;
using HRAcuity.Persistence;
using HRAcuity.Persistence.Quotes;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration.Mocks;

public class NotableQuoteDataSeeder(
    IDbContextFactory<HrAcuityDbContext> dbContextFactory,
    IStringHasher stringHasher)
{
    public async Task SeedAsync(bool useLargeDb, CancellationToken ct)
    {
        var notableQuotesProvider = new NotableQuoteTestDataProvider(useLargeDb);
        var commandHandler = new InsertNotableQuoteCommandHandler(
            dbContextFactory, stringHasher);

        // Map notable quotes to commands
        var quotes =
            notableQuotesProvider.NotableQuotes
                .Select(nq => new InsertNotableQuoteCommand(
                    nq.Author, nq.Quote))
                .ToList();

        // Initialize command
        var command = new InsertNotableQuotesCommand();
        command.AddRange(quotes);
        await commandHandler.HandleAsync(command, ct);
    }
}