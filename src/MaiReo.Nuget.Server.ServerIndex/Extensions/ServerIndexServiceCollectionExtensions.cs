using MaiReo.Nuget.Server.Core;
using MaiReo.Nuget.Server.Middlewares;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServerIndexServiceCollectionExtensions
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
