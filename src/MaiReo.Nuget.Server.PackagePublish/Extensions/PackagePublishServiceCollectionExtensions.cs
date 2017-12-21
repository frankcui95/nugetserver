using MaiReo.Nuget.Server;
using MaiReo.Nuget.Server.Core;
using MaiReo.Nuget.Server.Middlewares;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class
    PackagePublishServiceCollectionExtensions
    {
        public static IServiceCollection
        AddNugetServerPackagePublish(
            this IServiceCollection services,
            string url = null )
        {
            services.TryAddSingleton
                <IPackageStatusProvider, NullUnListPackageStatusProvider>();

            services.TryAddTransient
                <NugetServerPackagePublishMiddleware>();

            services.AddNugetServerCore(
                opt => opt.Resources
                .Add( NugetServerResourceType.PackagePublish,
                    url ?? "/package" ) );
            return services;
        }
    }
}
