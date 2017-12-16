using MaiReo.Nuget.Server.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder
        UseNugetServerRegistrationsBaseUrl(
            this IApplicationBuilder app)
            =>
            app
            .UseMiddleware
            <RegistrationsBaseUrlMiddleware>();
    }
}