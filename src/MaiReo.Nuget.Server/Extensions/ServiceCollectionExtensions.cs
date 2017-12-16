using MaiReo.Nuget.Server.Configurations;
using MaiReo.Nuget.Server.Configurations.Extensions;
using MaiReo.Nuget.Server.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
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
