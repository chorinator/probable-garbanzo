using HRAcuity.Application.Contracts;
using HRAcuity.Application.Features.Quotes.Commands;
using HRAcuity.Persistence.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HRAcuity.Presentation.WebApi.Endpoints.NotableQuotes;

public static class UpdateQuote
{
    internal static async Task<IResult> UpdateNotableQuote(
        [FromServices] ICommandHandlerAsync<UpdateNotableQuoteCommand> handler,
        int id, [FromBody] UpdateQuoteDto dto,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.Author))
            return Results.BadRequest("Author must be provided");
        if (string.IsNullOrWhiteSpace(dto.Quote))
            return Results.BadRequest("Quote must be provided");
        if (dto.Author.Length > 100)
            return Results.BadRequest("Author must be less than 100 characters");
        if (dto.Quote.Length > 500)
            return Results.BadRequest("Quote must be less than 500 characters");

        var command = new UpdateNotableQuoteCommand(id, dto.Author, dto.Quote);

        try
        {
            await handler.HandleAsync(command, ct);
        }
        catch (NotFoundException)
        {
            return Results.NotFound();
        }
        catch (Exception)
        {
            return Results.InternalServerError();
        }

        return Results.Accepted(id.ToString());
    }

    internal record UpdateQuoteDto(string Author, string Quote);
}