using HRAcuity.Application.Features.Quotes.Entities;
using HRAcuity.Application.Features.Quotes.Queries;

namespace HRAcuity.Persistence.Quotes;

public class GetAllNotableQuotesQueryHandler(IDbContextFactory<HrAcuityDbContext> dbContextFactory)
    : IQueryHandlerAsync<GetAllNotableQuotesQuery, IEnumerable<NotableQuote>>
{
    public async Task<IEnumerable<NotableQuote>> HandleAsync(GetAllNotableQuotesQuery request,
        CancellationToken ct)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(ct);

        var result = await dbContext.NotableQuotes
            .OrderBy(nq => nq.Id)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(ct);

        return result.Select(nq => new NotableQuote(nq.Id, nq.Author, nq.Quote));
    }
}