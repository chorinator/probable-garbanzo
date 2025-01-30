using HRAcuity.Application.Features.Quotes.Queries;
using HRAcuity.Persistence.Postgres.Quotes;

namespace Tests.Integration;

public class PostgresNotableQuoteUniquePairCounterHandlerTests
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
        var request = new NotableQuoteLengthQuery(maxLength);

        // Act
        var result =
            await Sut!.HandleAsync(request, Cts.Token);

        // Assert
        Assert.Equal(expected, result.First().Matches);
    }
    
    [Fact]
    public async Task Should_Return_NotableQuotePairLengthResult_LargeDb()
    {
        // Arrange
        await Seeder!.SeedAsync(true, Cts.Token);
        var request = new NotableQuoteLengthQuery(1);

        // Act
        var result =
            await Sut!.HandleAsync(request, Cts.Token);

        // Assert
        Assert.Equal(0, result.First().Matches);
    }
}