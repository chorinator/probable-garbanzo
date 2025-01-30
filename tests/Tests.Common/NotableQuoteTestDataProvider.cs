using System.Text.Json;
using HRAcuity.Application.Features.Quotes.Entities;

namespace Tests.Common;

public class NotableQuoteTestDataProvider
{
    private readonly List<NotableQuote> _notableQuotes;
    private const string FilePathFormat = "Resources/{0}Db.json";

    private record SeedDataSchema(int Id, string Author, string Text);

    private readonly JsonSerializerOptions _jsonSerializerOptions =
        new()
        {
            PropertyNameCaseInsensitive = true
        };

    public IReadOnlyList<NotableQuote> NotableQuotes => _notableQuotes;
    
    public NotableQuoteTestDataProvider(bool useLargeDb = false)
    {
        var filePath = string.Format(FilePathFormat, useLargeDb ? "Large" : "Short");
        var fileContentsAsText = File.ReadAllText(filePath);
        var quotes = JsonSerializer
                         .Deserialize<List<SeedDataSchema>>(fileContentsAsText, _jsonSerializerOptions)
                     ?? throw new Exception("Failed to deserialize quotes");

        _notableQuotes = quotes
            .Select(q =>
                new NotableQuote(q.Id, q.Author, q.Text))
            .ToList();
    }
}