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
            .AddNugetServerCore()
            .AddNugetServerIndex()
            .AddNugetServerPackagePublish()
            .AddNugetServerSearchQueryService()
            .AddNugetServerRegistrationsBaseUrl()
            .AddNugetServerPackageBaseAddress();
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
