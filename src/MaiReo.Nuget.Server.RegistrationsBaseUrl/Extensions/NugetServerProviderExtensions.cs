using MaiReo.Nuget.Server.Core;
using MaiReo.Nuget.Server.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.RegistrationsBaseUrl
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class NugetServerProviderExtensions
    {
        public static async Task RespondAsync(
            this INugetServerProvider provider,
            HttpContext context)
        {
            var excludeSemVer2Package = true;
            var useGzip = false;
            if (provider.IsMatchResource(context,
                NugetServerResourceType.RegistrationsBaseUrl_3_6_0))
            {
                excludeSemVer2Package = false;
            }
            if (provider.IsMatchResource(context,
                NugetServerResourceType.RegistrationsBaseUrl_3_4_0))
            {
                useGzip = true;
            }
            var req = provider.GetRequestingMetadata(context);
            if (req == null)
            {
                provider.RespondNotFound(context);
                return;
            }
            var metadatas = provider.NuspecProvider.GetAll()
            .Where(nuspec => nuspec.Metadata != null)
            .Select(nuspec => nuspec.Metadata)
            .Where(m => m.Id.ToLowerInvariant()
                    == req.Id.ToLowerInvariant())
            .Where(m => excludeSemVer2Package == false
                    || !((NuGetVersionString)m.Version).IsSemVer2);
            if (!metadatas.Any())
            {
                provider.RespondNotFound(context);
                return;
            }
            if (req.Version == null) // requesting index
            {
                await provider.RespondRegistrationIndexAsync(context, metadatas, useGzip: useGzip);
                return;
            }
            var metadata = metadatas.FirstOrDefault(m => (NuGetVersionString)m.Version == req.Version);
            if (metadata == null)
            {
                provider.RespondNotFound(context);
                return;
            }
            await provider.RespondRegistrationMetadataAsync(context,
            metadata, useGzip: useGzip);
        }
        public static async Task RespondRegistrationMetadataAsync(
            this INugetServerProvider provider,
            HttpContext context, NuspecMetadata metadata,
            bool useGzip = false)
        {
            var model = new MetadataModel
            {
            };
            await provider.WriteJsonResponseAsync(context, model, useGzip: useGzip);
        }

        public static async Task RespondRegistrationIndexAsync(
            this INugetServerProvider provider,
            HttpContext context, IEnumerable<NuspecMetadata> metadatas,
            bool useGzip = false)
        {
            var model = new RegistrationIndexOutputModel
            {
            };
            await provider.WriteJsonResponseAsync(context, model, useGzip: useGzip);
        }


        public static RequestingMetadataModel GetRequestingMetadata(
            this INugetServerProvider provider,
            HttpContext context)
        {
            var model = new RequestingMetadataModel();
            var paths = context.Request.Path.ToString().Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (paths.Length < 4)
            {
                return null;
            }
            if (!paths[3].EndsWith(".json", StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }
            model.Id = paths[2].ToLowerInvariant();
            var name = paths[3].Substring(0, paths[3].LastIndexOf('.'));
            if (string.Equals("index", name, StringComparison.InvariantCultureIgnoreCase))
            {
                return model;
            }
            if (!NuGetVersionString.TryParse(name, out var version))
            {
                return null;
            }
            model.Version = version;
            return model;
        }

        public static bool IsMatch(
            this INugetServerProvider provider,
            HttpContext context)
        {
            if (!provider.IsMatchVerbs(context,
                HttpMethods.IsHead,
                HttpMethods.IsGet))
            {
                return false;
            }
            return provider.IsMatchResources(
                context,
                NugetServerResourceType.RegistrationsBaseUrl,
                NugetServerResourceType.RegistrationsBaseUrl_3_0_0_beta,
                NugetServerResourceType.RegistrationsBaseUrl_3_0_0_rc
                /*
                 * Gzip does NOT work.
                 */
                //,
                //NugetServerResourceType.RegistrationsBaseUrl_3_4_0,
                //NugetServerResourceType.RegistrationsBaseUrl_3_6_0
                );
        }
    }
}
