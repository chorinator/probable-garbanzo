namespace HRAcuity.Application.Contracts;

public interface IQueryHandlerAsync<in TRequest, TResponse>
    where TRequest : IQuery<TResponse>
    where TResponse : class
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken ct);
}

public interface IQuery<T> where T : class;