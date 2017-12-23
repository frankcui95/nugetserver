using MaiReo.Nuget.Server.Middlewares;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class V2CompatibleApplicationBuilderExtensions
    {
        public static IApplicationBuilder
               UseNugetServerPackagePublishV2Compatible(
               this IApplicationBuilder app )
               => app.UseMiddleware
               <NugetServerPackagePublishV2CompatibleMiddleware>();
    }
}
