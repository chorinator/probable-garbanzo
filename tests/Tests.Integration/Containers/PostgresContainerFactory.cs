namespace Tests.Integration.Containers;

public class PostgresContainerFactory : ContainerFactory<PostgresContainer>
{
    protected override PostgresContainer GetContainerAsync() => new();
}