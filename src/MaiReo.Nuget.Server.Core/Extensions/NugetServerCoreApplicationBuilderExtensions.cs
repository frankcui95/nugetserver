using MaiReo.Nuget.Server.Core;
using MaiReo.Nuget.Server.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder

{
    public static class NugetServerCoreApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNugetServerCore(
            this IApplicationBuilder app )
        {
            var options = app.ApplicationServices.GetRequiredService<IOptions<NugetServerOptions>>().Value;
            var cacheProvider = app.ApplicationServices.GetRequiredService<ICacheProvider>();
            options.AddPackageCompleted -= cacheProvider.OnAddPackageCompleted;
            options.AddPackageCompleted += cacheProvider.OnAddPackageCompleted;
            options.DeletePackageCompleted -= cacheProvider.OnDeletePackageCompleted;
            options.DeletePackageCompleted += cacheProvider.OnDeletePackageCompleted;
            return app;
        }

    }
}