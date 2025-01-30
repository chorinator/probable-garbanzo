namespace Tests.Integration.Containers;

public interface ITestContainer : IAsyncDisposable
{
    Task InitializeAsync(CancellationToken ct);
}