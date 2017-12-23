using MaiReo.Nuget.Server.Middlewares;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class V2CompatibleServiceCollectionExtensions
    {
        public static IServiceCollection
        AddNugetServerPackagePublishV2Compatible(
            this IServiceCollection services )
        {
            services.TryAddTransient
                <NugetServerPackagePublishV2CompatibleMiddleware>();

            return services;
        }
    }
}
