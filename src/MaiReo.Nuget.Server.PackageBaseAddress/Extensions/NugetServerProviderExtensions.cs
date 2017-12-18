using MaiReo.Nuget.Server.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.PackageBaseAddress
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class NugetServerProviderExtensions
    {
        public static async Task RespondAsync(
            this INugetServerProvider provider,
            HttpContext context)
        {
            var fsRoot = provider.NuspecProvider.GetPackageRootFullPath();
            var fsRootUri = new Uri(fsRoot + "\\", UriKind.Absolute);
            var requestFileUri = provider.GetRequestFileUri(context);
            var fileUri = new Uri(fsRootUri, requestFileUri);
            var fileInfo = new PhysicalFileInfo(new FileInfo(fileUri.LocalPath));
            await context.Response.SendFileAsync(fileInfo, context.RequestAborted);
        }

        public static Uri GetRequestFileUri(
            this INugetServerProvider provider,
            HttpContext context)
        {
            var baseUrl = context.GetBaseUrl();
            var pkgBaseAddr = provider.GetResourceUrlPath(
                NugetServerResourceType.PackageBaseAddress);
            var pkgBaseUri = new Uri(baseUrl + pkgBaseAddr + "/", UriKind.Absolute);

            var pathUri = new Uri(baseUrl +
                context.Request.Path.ToUriComponent(), UriKind.Absolute);
            return pkgBaseUri.MakeRelativeUri(pathUri);
        }

        public static bool IsMatch(
            this INugetServerProvider provider,
            HttpContext context
            )
        {
            if (!provider.IsMatchVerbs(context,
                HttpMethods.IsHead,
                HttpMethods.IsGet))
            {
                return false;
            }

            return provider.IsMatchResource(
                context,
                NugetServerResourceType.PackageBaseAddress
                )
                && provider.IsMatchExtensionName(context, ".nupkg");

        }
    }
}
