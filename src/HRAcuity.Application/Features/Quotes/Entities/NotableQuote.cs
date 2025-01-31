namespace HRAcuity.Application.Features.Quotes.Entities;

public record NotableQuote(
    int Id,
    string Author,
    string Quote)
    : IEntity<int>
{
    public const int MinLength = 2;
}