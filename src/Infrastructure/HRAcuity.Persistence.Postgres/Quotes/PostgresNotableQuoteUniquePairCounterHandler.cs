using HRAcuity.Application.Contracts;
using HRAcuity.Application.Features.Quotes.Entities;
using HRAcuity.Application.Features.Quotes.Queries;
using Microsoft.EntityFrameworkCore;

namespace HRAcuity.Persistence.Postgres.Quotes;

public class PostgresNotableQuoteUniquePairCounterHandler(IDbContextFactory<HrAcuityDbContext> dbContextFactory)
    : IQueryHandlerAsync<NotableQuoteLengthQuery,
        IOrderedEnumerable<NotableQuoteLengthQuery.NotableQuotePairGroupsResult>>
{
    private const string Sql =
        """
        select nq."QuoteLength", count(distinct(nq."QuoteHash")) as UniqueQuotes
        from "NotableQuotes" nq
        where nq."QuoteLength" <= @maxLength
        group by nq."QuoteLength"
        """;

    public async Task<IOrderedEnumerable<NotableQuoteLengthQuery.NotableQuotePairGroupsResult>> HandleAsync(
        NotableQuoteLengthQuery request,
        CancellationToken ct)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(ct);
        await using var connection = dbContext.Database.GetDbConnection();

        await connection.OpenAsync(ct);
        await using var command = connection.CreateCommand();
        command.CommandText = Sql;

        // Parameter
        var maxLengthParam = command.CreateParameter();
        maxLengthParam.ParameterName = "maxLength";
        maxLengthParam.Value = request.MaxLength - NotableQuote.MinLength; 
        command.Parameters.Add(maxLengthParam);

        // Execute scalar and cast to long (or int, depending on your count size)
        var reader = await command.ExecuteReaderAsync(ct);
        var results = new List<NotableQuoteLengthQuery.NotableQuotePairGroupsResult>();
        while (await reader.ReadAsync(ct))
        {
            var key = reader.GetInt32(0);
            var value = reader.GetInt32(1);
            results.Add(
                new NotableQuoteLengthQuery.NotableQuotePairGroupsResult(key, value));
        }

        // Return or use totalCompliantPairs as needed
        return results.OrderBy(r => r.Length);
    }
}