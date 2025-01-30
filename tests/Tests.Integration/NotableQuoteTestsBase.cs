using System.Diagnostics;
using HRAcuity.Application.Contracts;
using HRAcuity.Application.Helpers;
using HRAcuity.Persistence.Postgres;
using Microsoft.EntityFrameworkCore;
using Tests.Integration.Mocks;

namespace Tests.Integration;

public abstract class NotableQuoteTestsBase : IAsyncLifetime
{
    private PostgresContainer? _container;

    protected readonly CancellationTokenSource Cts =
        Debugger.IsAttached
            ? new CancellationTokenSource(TimeSpan.FromHours(1))
            : new CancellationTokenSource(TimeSpan.FromMinutes(10)); // LargeDb takes a while to seed

    private readonly IStringHasher _stringHasher = new Md5StringHasher();
    protected NotableQuoteDataSeeder? Seeder;
    protected HrAcuityDbContextFactoryMock? MockDbContextFactory;

    public virtual async Task InitializeAsync()
    {
        // Init Container
        var postgresContainerFactory = new PostgresContainerFactory();
        _container = await postgresContainerFactory.CreateAndInitializeAsync(Cts.Token);

        // Init DbContext
        var dbContextFactory = new HRAcuityDbContextFactory();
        var connectionString = _container.GetConnectionString();
        await using var dbContext = 
            dbContextFactory.CreateDbContext([connectionString]);

        // Apply migrations
        await dbContext.Database.MigrateAsync(Cts.Token);

        // Init Seeder
        MockDbContextFactory =
            new HrAcuityDbContextFactoryMock(dbContextFactory, connectionString);
        Seeder = new NotableQuoteDataSeeder(MockDbContextFactory, _stringHasher);
    }

    public async Task DisposeAsync()
    {
        if (_container is not null)
            await _container.DisposeAsync();

        Cts.Dispose();
    }
}