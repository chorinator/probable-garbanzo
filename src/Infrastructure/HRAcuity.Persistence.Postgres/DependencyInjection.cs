using HRAcuity.Application.Contracts;
using HRAcuity.Application.Features.Quotes.Commands;
using HRAcuity.Application.Features.Quotes.Entities;
using HRAcuity.Application.Features.Quotes.Queries;
using HRAcuity.Application.Helpers;
using HRAcuity.Persistence.Postgres.Quotes;
using HRAcuity.Persistence.Quotes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HRAcuity.Persistence.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddSingleton<ICommandHandlerAsync<InsertNotableQuotesCommand>, InsertNotableQuoteCommandHandler>();
        services.AddSingleton<IStringHasher, Md5StringHasher>();
        services.AddSingleton<IQueryHandlerAsync<GetAllNotableQuotesQuery, IEnumerable<NotableQuote>>,
            GetAllNotableQuotesQueryHandler>();
        services.AddSingleton<ICommandHandlerAsync<DeleteNotableQuoteCommand>, DeleteNotableQuoteCommandHandler>();
        services.AddSingleton<ICommandHandlerAsync<UpdateNotableQuoteCommand>, UpdateNotableQuoteCommandHandler>();
        services.AddSingleton<
            IQueryHandlerAsync<NotableQuoteLengthQuery,
                IOrderedEnumerable<NotableQuoteLengthQuery.NotableQuotePairGroupsResult>>,
            PostgresNotableQuoteUniquePairCounterHandler>();

        services.AddDbContextFactory<HrAcuityDbContext>(
            o => o.UseNpgsql(
                connectionString, oo =>
                    oo.MigrationsAssembly(typeof(HRAcuityDbContextFactory).Assembly.FullName)
            ));

        var scopedProvider = services.BuildServiceProvider();
        using var scope = scopedProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HrAcuityDbContext>();
        dbContext.Database.Migrate();

        return services;
    }
}