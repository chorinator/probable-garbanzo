namespace HRAcuity.Application.Features.Quotes.Commands;

public record UpdateNotableQuoteCommand(int Id, string Author, string Quote) : ICommand;