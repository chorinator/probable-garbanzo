namespace HRAcuity.Application.Features.Quotes.Queries;

public record GetQuoteByIdQuery(int Id) : IQuery<NotableQuote>;