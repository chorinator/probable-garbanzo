using HRAcuity.Application.Contracts;
using HRAcuity.Application.Features.Quotes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HRAcuity.Presentation.WebApi.Endpoints.NotableQuotes;

public static class GetPairs
{
    internal static async Task<IResult> GetComplyingPairs(
        [FromServices]
        IQueryHandlerAsync<NotableQuoteLengthQuery, NotableQuoteLengthQuery.NotableQuotePairsResult> queryHandler,
        [FromQuery] int maxLength,
        CancellationToken ct)
    {
        var result =
            await queryHandler.HandleAsync(new NotableQuoteLengthQuery(maxLength), ct);

        return Results.Ok(result.First());
    }
}