using MaiReo.Nuget.Server.Configurations;
using MaiReo.Nuget.Server.Middlewares;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
            .AddNugetServerCore(setupAction)
            .TryAddTransient<NugetServerIndexMiddleware>();

            return services;
        }
    }
}
