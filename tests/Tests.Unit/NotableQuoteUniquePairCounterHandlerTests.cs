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
        Assert.Equal(expected, result.Count());
    }

    [Fact]
    public async Task Should_Fail_NotableQuotePairLengthResult_LargeDb()
    {
        // Arrange
        var shortDbMock = new NotableQuoteHandlerMock(useLargeDb: true);
        var sut = new NotableQuoteUniquePairCounterHandler(shortDbMock);
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var request = new NotableQuoteLengthQuery(10);
        var token = cts.Token;

        // Act
        var result =
            await Record.ExceptionAsync(() => sut.HandleAsync(request, token));

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OperationCanceledException>(result);
    }
}