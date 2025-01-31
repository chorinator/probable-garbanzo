using HRAcuity.Application.Features.Quotes.Handlers;
using HRAcuity.Application.Features.Quotes.Queries;

namespace Tests.Unit;

public class NotableQuoteUniquePairCounterHandlerTests
{
    [Theory]
    [InlineData(19, 0)]
    [InlineData(22, 1)]
    [InlineData(32, 6)]
    [InlineData(40, 7)]
    [InlineData(200, 21)]
    public async Task Should_Return_NotableQuotePairLengthResult_ShortDb(int maxLength, int expected)
    {
        // Arrange
        var shortDbMock = new NotableQuoteHandlerMock(false);
        var sut = new NotableQuoteUniquePairCounterHandler(shortDbMock);
        using var cts = new CancellationTokenSource();
        var request = new NotableQuoteLengthQuery(maxLength);

        // Act
        var result = await sut.HandleAsync(request, cts.Token);

        // Assert
        var matches = result.Matches;
        Assert.Equal(expected, matches);
    }

    [Theory]
    [InlineData(10, 1745672679)]
    [InlineData(5, 7642518)]
    [InlineData(1, 0)]
    public async Task Should_Return_NotableQuotePairLengthResult_LargeDb(int maxLength, int expected)
    {
        // Arrange
        var shortDbMock = new NotableQuoteHandlerMock(useLargeDb: true);
        var sut = new NotableQuoteUniquePairCounterHandler(shortDbMock);
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var request = new NotableQuoteLengthQuery(maxLength);

        // Act
        var result = await sut.HandleAsync(request, cts.Token);

        // Assert
        var matches = result.Matches;
        Assert.Equal(expected, matches);
    }
}