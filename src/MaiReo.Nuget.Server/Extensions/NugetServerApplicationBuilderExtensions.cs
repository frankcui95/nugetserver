using System;
using MaiReo.Nuget.Server.Core;

namespace Microsoft.AspNetCore.Builder
{
    public static class NugetServerApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNugetServer(
            this IApplicationBuilder app)
            => app
            .UseNugetServerCore()
            .UseNugetServerIndex()
            .UseNugetServerPackageBaseAddress()
            .UseNugetServerPackagePublish()
            .UseNugetServerPackagePublishV2Compatible()
            .UseNugetServerRegistrationsBaseUrl()
            .UseNugetServerSearchQueryService();
    }
}