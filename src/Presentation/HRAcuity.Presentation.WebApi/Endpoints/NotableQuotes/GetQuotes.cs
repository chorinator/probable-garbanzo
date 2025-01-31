using System.ComponentModel.DataAnnotations;
using HRAcuity.Application.Contracts;
using HRAcuity.Application.Features.Quotes.Entities;
using HRAcuity.Application.Features.Quotes.Queries;
using HRAcuity.Presentation.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HRAcuity.Presentation.WebApi.Endpoints.NotableQuotes;

public static class GetQuotes
{
    internal static async Task<IResult> GetAllQuotes(
        [FromServices] IQueryHandlerAsync<GetAllNotableQuotesQuery, IEnumerable<NotableQuote>> queryHandler,
        PaginationDto p,
        CancellationToken ct)
    {
        if (p.Page < 1)
            return Results.BadRequest("Page must be greater than 0");
        if (p.PageSize < 1)
            return Results.BadRequest("Page size must be greater than 0");

        var result =
            await queryHandler.HandleAsync(new GetAllNotableQuotesQuery(p.Page, p.PageSize), ct);

        return TypedResults.Ok(result);
    }

    internal static async Task<IResult> ById(
        [FromServices] IQueryHandlerAsync<GetQuoteByIdQuery, NotableQuote> queryHandler,
        int id,
        CancellationToken ct)
    {
        if (id < 1)
            return Results.BadRequest("Id must be greater than 0");

        var result = await queryHandler.HandleAsync(new GetQuoteByIdQuery(id), ct);

        return TypedResults.Ok(result);
    }
}