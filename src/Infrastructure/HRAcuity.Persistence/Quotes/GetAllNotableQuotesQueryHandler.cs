using HRAcuity.Application.Features.Quotes.Entities;
using HRAcuity.Application.Features.Quotes.Queries;

namespace HRAcuity.Persistence.Quotes;

public class GetAllNotableQuotesQueryHandler(IDbContextFactory<HrAcuityDbContext> dbContextFactory)
    : IQueryHandlerAsync<GetAllNotableQuotesQuery, NotableQuote>
{
    public async Task<IEnumerable<NotableQuote>> HandleAsync(GetAllNotableQuotesQuery request,
        CancellationToken ct)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(ct);

        var result = await dbContext.NotableQuotes
            .Skip(request.Page - 1)
            .Take(request.PageSize)
            .ToListAsync(ct);

        return result.Select(nq => new NotableQuote(nq.Id, nq.Author, nq.Quote));
    }
}