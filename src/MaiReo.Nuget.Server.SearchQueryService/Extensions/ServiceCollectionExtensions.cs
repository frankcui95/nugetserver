using MaiReo.Nuget.Server;
using MaiReo.Nuget.Server.Middlewares;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection
        AddNugetServerSearchQueryService(
            this IServiceCollection services,
            string url = null)
        {
            services
            .AddNugetServerCore(
                opt => opt
                .Resources
                .Add(NugetServerResourceTypes.SearchQueryService,
                url ?? "/query"))
            .TryAddTransient<NugetServerSearchQueryServiceMiddleware>(); 

            return services;
        }
    }
}
