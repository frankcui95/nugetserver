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
        public static IServiceCollection AddNugetServerCore(
            this IServiceCollection services,
            Action<NugetServerOptions> setupAction = null)
        {
            services
            .Configure(setupAction ?? (opts => { }))
            .TryAddTransient<INugetServerProvider,NugetServerProvider>();

            return services;
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
