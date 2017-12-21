using MaiReo.Nuget.Server.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CacheServiceCollectionExtensions
    {
        public static IServiceCollection AddNugetServerCache(
           this IServiceCollection services,
           Action<NugetServerOptions> setupAction = null )
        {
            services.TryAddSingleton<ICacheProvider, InMemoryCacheProvider>();
            services.ConfigureNugetServer( options => options.CacheEnabled = true );
            services.AddNugetServerCore( setupAction ?? (opts => { }) );

            return services;
        }
    }
}
