using MaiReo.Nuget.Server;
using MaiReo.Nuget.Server.Core;
using MaiReo.Nuget.Server.Middlewares;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class
    SearchQueryServiceCollectionExtensions
    {
        public static IServiceCollection
        AddNugetServerSearchQueryService(
            this IServiceCollection services,
            string url = null)
        {
            services.AddNugetServerCore(
                opt => opt.Resources
                    .Add(NugetServerResourceType
                         .SearchQueryService,
                         url ?? "/query"));

            services.TryAddTransient
            <INuspecProvider, ZipFileNuspecProvider>();

            services.TryAddTransient
            <NugetServerSearchQueryServiceMiddleware>();

            return services;
        }
    }
}
