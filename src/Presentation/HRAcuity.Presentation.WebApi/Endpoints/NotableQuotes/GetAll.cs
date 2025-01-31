using HRAcuity.Application.Contracts;
using HRAcuity.Application.Features.Quotes.Entities;
using HRAcuity.Application.Features.Quotes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HRAcuity.Presentation.WebApi.Endpoints.NotableQuotes;

public static class GetAll
{
    internal static async Task<IResult> GetAllQuotes(
        [FromServices] IQueryHandlerAsync<GetAllNotableQuotesQuery, IEnumerable<NotableQuote>> queryHandler,
        [FromQuery] int page, [FromQuery] int pageSize,
        CancellationToken ct)
    {
        if (page < 1)
            return Results.BadRequest("Page must be greater than 0");
        if (pageSize < 1)
            return Results.BadRequest("Page size must be greater than 0");

        var result =
            await queryHandler.HandleAsync(new GetAllNotableQuotesQuery(page, pageSize), ct);

        return TypedResults.Ok(result);
    }
}