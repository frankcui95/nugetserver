using MaiReo.Nuget.Server.Core;
using MaiReo.Nuget.Server.Models;
using MaiReo.Nuget.Server.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
            var req = provider
                .GetRequstingPackageBaseAddressModel(
                    context);
            if (!req.IsValid())
            {
                provider.RespondNotFound(context);
                return;
            }
            if (req.IsRequestingVersions())
            {
                await provider.RespondVersionsAsync(context, req);
                return;
            }
            if (req.IsRequestingNuspec())
            {
                await provider.RespondNuspecAsync(context, req);
                return;
            }
            if (req.IsRequestingNupkg())
            {
                await provider.RespondNupkgAsync(context, req);
                return;
            }

            provider.RespondNotFound(context);
        }
        public static async Task RespondNuspecAsync(
            this INugetServerProvider provider,
            HttpContext context,
            PackageBaseAddressInputModel request)
        {
            var nuspec = provider.NuspecProvider
                .GetAll()
                .Where(n => n.Metadata != null)
                .Where(n => n.Metadata.Id.ToLowerInvariant() == request.Id)
                .FirstOrDefault(n => n.Metadata.Version == request.Version);
            var nuspecRaw = await Zip.ReadNuspecRawAsync(nuspec);

            await provider.WriteRawResponseAsync(
                context, "application/xml; charset=utf8", nuspecRaw);
        }
        public static async Task RespondVersionsAsync(
            this INugetServerProvider provider,
            HttpContext context,
            PackageBaseAddressInputModel request)
        {
            var model = new PackageBaseAddressVersionsOutputModel
            {
                Versions = provider.NuspecProvider
                .GetAll()
                .Where(n => n.Metadata != null)
                .Where(n => n.Metadata.Id.ToLowerInvariant() == request.Id)
                .Select(n => n.Metadata.Version)
                .OrderBy(x => (NuGetVersionString)x)
                .ToList()
            };
            await provider.WriteJsonResponseAsync(context, model);
        }

        public static async Task RespondNupkgAsync(
            this INugetServerProvider provider,
            HttpContext context,
            PackageBaseAddressInputModel request)
        {
            var fsRoot = provider.NuspecProvider
                    .GetPackageRootFullPath();
            var fsRootUri = new Uri(fsRoot + "\\",
                    UriKind.Absolute);
            var fileUri = request
                .GetRequestFileUri(fsRootUri);
            var fileInfo = new PhysicalFileInfo(
                new FileInfo(fileUri.LocalPath));
            await context.Response.SendFileAsync(
                fileInfo, context.RequestAborted);
        }

        public static PackageBaseAddressInputModel
            GetRequstingPackageBaseAddressModel(
            this INugetServerProvider provider,
            HttpContext context)
            => new PackageBaseAddressInputModel(
                context.GetBaseUrl(), context.Request.Path,
                provider.GetResourceUrlPath(
                NugetServerResourceType.PackageBaseAddress));

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
                );

        }
    }
}
