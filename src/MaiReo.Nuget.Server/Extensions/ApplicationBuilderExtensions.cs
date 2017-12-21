using System;
using MaiReo.Nuget.Server.Core;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNugetServer(
            this IApplicationBuilder app)
            => app
            .UseNugetServerCore()
            .UseNugetServerIndex()
            .UseNugetServerPackageBaseAddress()
            .UseNugetServerPackagePublish()
            .UseNugetServerRegistrationsBaseUrl()
            .UseNugetServerSearchQueryService();
    }
}