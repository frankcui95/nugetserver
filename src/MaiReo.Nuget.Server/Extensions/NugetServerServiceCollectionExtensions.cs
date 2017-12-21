using MaiReo.Nuget.Server.Core;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NugetServerServiceCollectionExtensions
    {
        public static IServiceCollection AddNugetServer(
            this IServiceCollection services,
            Action<NugetServerOptions> setupAction = null)
        {
            return services
            .AddNugetServerCache()
            .AddNugetServerPackagePublish()
            .AddNugetServerPackageBaseAddress()
            .AddNugetServerRegistrationsBaseUrl()
            .AddNugetServerSearchQueryService()
            .AddNugetServerIndex()
            .AddNugetServerCore( setupAction );
        }

        public static IServiceCollection ConfigureNugetServer(
            this IServiceCollection services,
            Action<NugetServerOptions> setupAction)
            =>
            services
            .PostConfigure(setupAction 
                ?? throw new ArgumentNullException(nameof(setupAction)));
    }
}
