using MaiReo.Nuget.Server.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NugetServerCoreServiceCollectionExtensions
    {
        public static IServiceCollection AddNugetServerCore(
            this IServiceCollection services,
            Action<NugetServerOptions> setupAction = null )
        {
            services
            .Configure( setupAction ?? (opts => { }) );

            services.TryAddSingleton
                <INupkgProvider, NupkgProvider>();

            services.TryAddSingleton
                <INuspecProvider, ZipFileNuspecProvider>();

            services.TryAddSingleton<ZipFileNuspecProvider>();

            services.TryAddSingleton
               <ICacheProvider, NoCacheProvider>();

            services.TryAddSingleton<NoCacheProvider>();

            services.TryAddSingleton
                <IPackageStatusProvider, UnListPackageStatusProvider>();

            services.TryAddSingleton<UnListPackageStatusProvider>();

            services.TryAddSingleton
                <INugetServerProvider, NugetServerProvider>();

            services.TryAddSingleton<NugetServerProvider>();

            return services;
        }

        public static IServiceCollection ConfigureNugetServer(
            this IServiceCollection services,
            Action<NugetServerOptions> setupAction )
            =>
            services
            .PostConfigure( setupAction
                ?? throw new ArgumentNullException( nameof( setupAction ) ) );
    }
}
