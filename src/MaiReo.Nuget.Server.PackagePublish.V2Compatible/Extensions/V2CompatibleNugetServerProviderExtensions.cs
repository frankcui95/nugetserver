using MaiReo.Nuget.Server.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.Extensions
{
    public static class V2CompatibleNugetServerProviderExtensions
    {
        public static Task RespondPackagePublishV2CompatibleAsync(
            this INugetServerProvider provider,
            HttpContext context )
        {
            provider.RedirectToCurrentApiVersion( context );
            return Task.CompletedTask;
        }

        public static bool IsMatchPackagePublishV2Compatible(
            this INugetServerProvider provider,
            HttpContext context )
        {
            if (!provider.IsMatchVerbs( context,
                HttpMethods.IsPut,
                HttpMethods.IsDelete ))
            {
                return false;
            }

            return provider.IsMatchResourceIgnoreApiVersion(
                context,
                NugetServerResourceType.PackagePublish
                );
        }
    }
}
