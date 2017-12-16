using System;
using MaiReo.Nuget.Server.Core;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNugetServerRegistrationsBaseUrl(
            this IApplicationBuilder app)
            =>
            app
            .UseMiddleware<NugetServerRegistrationsBaseUrlMiddleware>();
    }
}