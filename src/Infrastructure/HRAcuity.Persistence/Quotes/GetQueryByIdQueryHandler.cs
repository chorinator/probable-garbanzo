using HRAcuity.Application.Features.Quotes.Entities;
using HRAcuity.Application.Features.Quotes.Queries;
using HRAcuity.Persistence.Exceptions;

namespace HRAcuity.Persistence.Quotes;

public class GetQueryByIdQueryHandler(IDbContextFactory<HrAcuityDbContext> dbContextFactory)
    : IQueryHandlerAsync<GetQuoteByIdQuery, NotableQuote>
{
    public async Task<NotableQuote> HandleAsync(GetQuoteByIdQuery request, CancellationToken ct)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(ct);
        var quote = await dbContext.NotableQuotes.FindAsync([request.Id], cancellationToken: ct);

        if (quote is null)
            throw new NotFoundException("Notable quote not found.");

        return new NotableQuote(quote.Id, quote.Author, quote.Quote);
    }
}