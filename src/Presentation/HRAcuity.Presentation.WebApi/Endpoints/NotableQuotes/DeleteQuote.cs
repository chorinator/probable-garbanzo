using HRAcuity.Application.Contracts;
using HRAcuity.Application.Features.Quotes.Commands;
using HRAcuity.Persistence.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HRAcuity.Presentation.WebApi.Endpoints.NotableQuotes;

public static class DeleteQuote
{
    internal static async Task<IResult> DeleteNotableQuote(
        [FromServices] ICommandHandlerAsync<DeleteNotableQuoteCommand> handler,
        int id,
        CancellationToken ct)
    {
        var command = new DeleteNotableQuoteCommand(id);

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
}