using MaiReo.Nuget.Server.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class PackagePublishApplicationBuilderExtensions
    {
        public static IApplicationBuilder
            UseNugetServerPackagePublish(
            this IApplicationBuilder app)
            => app.UseMiddleware
            <NugetServerPackagePublishMiddleware>();
    }
}