using MaiReo.Nuget.Server;

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
                url ?? "query"));

            return services;
        }
    }
}
