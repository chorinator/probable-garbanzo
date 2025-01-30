using Testcontainers.PostgreSql;

namespace Tests.Integration.Containers;

public class PostgresContainer : ITestContainer
{
    protected readonly PostgreSqlContainer MyPostgresContainer = new PostgreSqlBuilder().Build();

    public async Task InitializeAsync(CancellationToken ct) =>
        await MyPostgresContainer.StartAsync(ct);

    public async ValueTask DisposeAsync() =>
        await MyPostgresContainer.DisposeAsync();

    public string GetConnectionString() =>
        MyPostgresContainer.GetConnectionString();
}