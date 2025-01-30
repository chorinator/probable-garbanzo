using HRAcuity.Application.Contracts;
using HRAcuity.Application.Features.Quotes.Queries;
using Microsoft.EntityFrameworkCore;

namespace HRAcuity.Persistence.Postgres.Quotes;

public class PostgresNotableQuoteUniquePairCounterHandler(IDbContextFactory<HrAcuityDbContext> dbContextFactory)
    : IQueryHandlerAsync<NotableQuoteLengthQuery, NotableQuoteLengthQuery.NotableQuotePairsResult>
{
    private const string Sql =
        """
            SELECT COUNT(1)
            FROM (SELECT * FROM "NotableQuotes" nq 
                  WHERE nq."QuoteLength" < @maxLength) q1
            JOIN (SELECT * FROM "NotableQuotes" nq 
                  WHERE nq."QuoteLength" < @maxLength) q2
              ON q2."QuoteLength" <= @maxLength - q1."QuoteLength"
            WHERE q1."Id" < q2."Id"
              AND q1."QuoteHash" <> q2."QuoteHash"
        """;

    public async Task<IEnumerable<NotableQuoteLengthQuery.NotableQuotePairsResult>> HandleAsync(
        NotableQuoteLengthQuery request,
        CancellationToken ct)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(ct);
        var connection = dbContext.Database.GetDbConnection();

        await connection.OpenAsync(ct);
        await using var command = connection.CreateCommand();
        command.CommandText = Sql;

        // Parameter
        var maxLengthParam = command.CreateParameter();
        maxLengthParam.ParameterName = "maxLength";
        maxLengthParam.Value = request.MaxLength;
        command.Parameters.Add(maxLengthParam);

        // Execute scalar and cast to long (or int, depending on your count size)
        var countObj = await command.ExecuteScalarAsync(ct);
        var totalCompliantPairs = Convert.ToInt64(countObj);

        // Return or use totalCompliantPairs as needed
        var asList = new List<NotableQuoteLengthQuery.NotableQuotePairsResult>
            { new(totalCompliantPairs) };
        return asList;
    }
}