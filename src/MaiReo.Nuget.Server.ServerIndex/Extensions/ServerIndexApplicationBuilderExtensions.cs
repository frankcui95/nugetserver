using MaiReo.Nuget.Server.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class ServerIndexApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNugetServerIndex(
            this IApplicationBuilder app)
            =>
            app
            .UseMiddleware<NugetServerIndexMiddleware>();
    }
}