namespace HRAcuity.Application.Features.Quotes.Commands;

public record InsertNotableQuoteCommand(string Author, string Quote) : ICommand;

public class InsertNotableQuotesCommand :
    List<InsertNotableQuoteCommand>,
    ICommand;