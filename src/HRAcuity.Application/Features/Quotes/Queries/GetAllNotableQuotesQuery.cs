using HRAcuity.Application.Features.Quotes.Entities;

namespace HRAcuity.Application.Features.Quotes.Queries;

public record GetAllNotableQuotesQuery(int Page, int PageSize) : IQuery<NotableQuote>;