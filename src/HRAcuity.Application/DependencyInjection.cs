using HRAcuity.Application.Features.Quotes.Handlers;
using HRAcuity.Application.Features.Quotes.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace HRAcuity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services
            .AddSingleton<
                IQueryHandlerAsync<NotableQuoteLengthQuery, NotableQuoteLengthQuery.NotableQuotePairsResult>,
                NotableQuoteUniquePairCounterHandler>();

        return services;
    }
}