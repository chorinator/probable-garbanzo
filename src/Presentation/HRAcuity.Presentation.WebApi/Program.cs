using HRAcuity.Persistence.Postgres;
using HRAcuity.Presentation.WebApi.Endpoints.NotableQuotes;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");
if (string.IsNullOrWhiteSpace(connectionString))
    throw new Exception("No connection string found");

// Add services to the container.
// Add Persistence
builder.Services.AddPersistence(connectionString);

builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(o => 
        o.SwaggerEndpoint("/openapi/v1.json", "HRAcuity.Presentation.WebApi v1"));
}

app.UseHttpsRedirection();

// Register endpoints
app.RegisterNotableQuotesEndpoints();

app.Run();