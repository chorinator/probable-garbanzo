namespace HRAcuity.Application.Contracts;

public interface ICommandHandlerAsync<in TRequest>
    where TRequest : ICommand
{
    Task HandleAsync(TRequest request, CancellationToken ct);
}

public interface ICommand { }