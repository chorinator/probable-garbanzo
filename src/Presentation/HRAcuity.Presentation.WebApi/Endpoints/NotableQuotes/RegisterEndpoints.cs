namespace HRAcuity.Presentation.WebApi.Endpoints.NotableQuotes;

public static class RegisterEndpoints
{
    public static WebApplication RegisterNotableQuotesEndpoints(this WebApplication app)
    {
        var packageGroup = app.MapGroup("/api/NotableQuotes");

        packageGroup.MapGet("/GetPairs", GetPairs.GetComplyingPairs);
        packageGroup.MapGet("/", GetAll.GetAllQuotes);
        packageGroup.MapPost("/", InsertQuote.InsertNotableQuote);
        packageGroup.MapDelete("/{id:int}", DeleteQuote.DeleteNotableQuote);
        packageGroup.MapPut("/{id:int}", UpdateQuote.UpdateNotableQuote);

        return app;
    }
}