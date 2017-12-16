using System;
using MaiReo.Nuget.Server.Core;

namespace Microsoft.AspNetCore.Builder
{
    public static class NugetServerBuilderExtensions
    {
        public static IApplicationBuilder UseNugetServer(
            this IApplicationBuilder app)
            =>
            app
            .UseNugetServerIndex()
            .UseNugetServerPackageBaseAddress()
            .UseNugetServerPackagePublish()
            .UseNugetServerRegistrationsBaseUrl()
            .UseNugetServerSearchQueryService()
            ;
    }
}