namespace HRAcuity.Application.Features.Quotes.Entities;

public record PaginatedQuery<T>(int Page, int PageSize) :
    IQuery<T>
    where T : PaginatedResult;