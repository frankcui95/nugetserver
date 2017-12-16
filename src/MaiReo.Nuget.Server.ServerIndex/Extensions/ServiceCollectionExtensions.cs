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
        public static IServiceCollection AddNugetServerIndex(
            this IServiceCollection services,
            Action<NugetServerOptions> setupAction = null)
        {
            services
            .AddNugetServerCore(setupAction);

            return services;
        }
    }
}
