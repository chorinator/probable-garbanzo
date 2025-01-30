namespace Tests.Integration.Containers;

public abstract class ContainerFactory<TContainer>
    where TContainer : ITestContainer
{
    public async Task<TContainer> CreateAndInitializeAsync(CancellationToken ct)
    {
        var container = Create();
        await container.InitializeAsync(ct);

        return container;
    }

    public TContainer Create() => GetContainerAsync();

    protected abstract TContainer GetContainerAsync();
}