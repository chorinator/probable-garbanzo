using System.Diagnostics;
using HRAcuity.Application.Features.Quotes.Handlers;
using HRAcuity.Application.Features.Quotes.Queries;
using HRAcuity.Persistence.Postgres.Quotes;
using Xunit.Abstractions;

namespace Tests.Integration;

public class PostgresNotableQuoteUniquePairCounterHandlerTests(ITestOutputHelper logger)
    : NotableQuoteTestsBase
{
    private PostgresNotableQuoteUniquePairCounterHandler? Sut { get; set; }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        Sut = new PostgresNotableQuoteUniquePairCounterHandler(MockDbContextFactory!);
    }

    [Theory]
    [InlineData(19, 0)]
    [InlineData(22, 1)]
    [InlineData(32, 6)]
    [InlineData(40, 7)]
    [InlineData(200, 21)]
    public async Task Should_Return_NotableQuotePairLengthResult_ShortDb(int maxLength, int expected)
    {
        // Arrange
        await Seeder!.SeedAsync(false, Cts.Token);
        var sut = new NotableQuoteUniquePairCounterHandler(Sut!);
        var request = new NotableQuoteLengthQuery(maxLength);

        // Act
        var result =
            await sut.HandleAsync(request, Cts.Token);

        // Assert
        Assert.Equal(expected, result.Matches);
    }

    [Theory]
    [InlineData(10, 1745672679)]
    [InlineData(5, 7642518)]
    [InlineData(1, 0)]
    public async Task Should_Return_NotableQuotePairLengthResult_LargeDb(int maxLength, int expected)
    {
        // Arrange
        await Seeder!.SeedAsync(true, Cts.Token);
        var sut = new NotableQuoteUniquePairCounterHandler(Sut!);
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var request = new NotableQuoteLengthQuery(maxLength);
        var stopWatch = Stopwatch.StartNew();

        // Act
        logger.WriteLine($"Running test for {maxLength} length");
        var result = await sut.HandleAsync(request, Cts.Token);
        stopWatch.Stop();
        logger.WriteLine($"Test took {stopWatch.ElapsedMilliseconds} ms");

        // Assert
        Assert.Equal(expected, result.Matches);
    }
}