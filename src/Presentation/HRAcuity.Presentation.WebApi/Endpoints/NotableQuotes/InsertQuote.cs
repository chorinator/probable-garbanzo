using HRAcuity.Application.Contracts;
using HRAcuity.Application.Features.Quotes.Commands;
using Microsoft.AspNetCore.Mvc;

namespace HRAcuity.Presentation.WebApi.Endpoints.NotableQuotes;

public static class InsertQuote
{
    internal static async Task<IResult> InsertNotableQuote(
        [FromServices] ICommandHandlerAsync<InsertNotableQuotesCommand> handler,
        [FromQuery] string author, [FromQuery] string quote,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(author))
            return Results.BadRequest("Author must be provided");
        if (string.IsNullOrWhiteSpace(quote))
            return Results.BadRequest("Quote must be provided");
        if (author.Length > 100)
            return Results.BadRequest("Author must be less than 100 characters");
        if (quote.Length > 500)
            return Results.BadRequest("Quote must be less than 500 characters");

        var command = new InsertNotableQuoteCommand(author, quote);
        var asList = new InsertNotableQuotesCommand { command };

        await handler.HandleAsync(asList, ct);

        return Results.Created();
    }
}