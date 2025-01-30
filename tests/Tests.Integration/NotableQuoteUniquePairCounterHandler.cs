using HRAcuity.Application.Features.Quotes.Queries;

namespace Tests.Integration;

public class NotableQuoteUniquePairCounterHandler :
    NotableQuoteTestsBase
{
    private HRAcuity.Persistence.Quotes.NotableQuoteUniquePairCounterHandler? Sut { get; set; }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        Sut = new HRAcuity.Persistence.Quotes.NotableQuoteUniquePairCounterHandler(MockDbContextFactory!);
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
        var applicationHandler =
            new HRAcuity.Application.Features.Quotes.Handlers.NotableQuoteUniquePairCounterHandler(Sut!);

        // Act
        var result =
            await applicationHandler.HandleAsync(request, Cts.Token);

        // Assert
        Assert.Equal(expected, result.Count());
    }
    
    [Fact]
    public async Task Should_Fail_NotableQuotePairLengthResult_LargeDb()
    {
        // Arrange
        await Seeder!.SeedAsync(true, Cts.Token);
        var applicationHandler =
            new HRAcuity.Application.Features.Quotes.Handlers.NotableQuoteUniquePairCounterHandler(Sut!);
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var request = new NotableQuoteLengthQuery(10);
        var token = cts.Token;

        // Act
        var result =
            await Record.ExceptionAsync(() => applicationHandler.HandleAsync(request, token));

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OperationCanceledException>(result);
    }
}