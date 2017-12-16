using MaiReo.Nuget.Server.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNugetServerPackagePublish(
            this IApplicationBuilder app)
            =>
            app
            .UseMiddleware<NugetServerPackagePublishMiddleware>();
    }
}