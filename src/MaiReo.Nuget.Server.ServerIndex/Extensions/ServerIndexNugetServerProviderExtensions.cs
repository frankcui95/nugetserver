using MaiReo.Nuget.Server.Core;
using MaiReo.Nuget.Server.Models;
using MaiReo.Nuget.Server.Serialization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.ServerIndex
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ServerIndexNugetServerProviderExtensions
    {
        public static async Task RespondServerIndexAsync(
            this INugetServerProvider provider,
            HttpContext context)
        {
            var serverIndex = new ServerIndexModel(
                provider.ParseResource(context))
            {
                Version = provider
                    .NugetServerOptions
                    .ApiVersion
                    .ToFullString(),
                Context = ServerIndexContext.Default
            };
            await provider
                  .WriteJsonResponseAsync(
                    context, serverIndex, provider
                  .CreateJsonSerializerForServiceIndex());
        }

        private static JsonSerializer
        CreateJsonSerializerForServiceIndex(
            this INugetServerProvider provider)
        {
            var serializer = provider.CreateJsonSerializer();
            serializer.ContractResolver
                = new ServerIndexContractResolver();
            return serializer;
        }

        private static IEnumerable<ServerIndexResourceModel>
        ParseResource(this INugetServerProvider provider,
            HttpContext context)
        {
            var resources = provider
                ?.NugetServerOptions
                ?.Resources
                ?.Select(r => r.Key);

            if (resources == null)
            {
                yield break;
            }

            var baseUrl = context.GetBaseUrl();

            foreach (var resource in resources)
            {
                var model = new ServerIndexResourceModel
                {
                    Type = resource.GetText(),
                    Id = baseUrl + provider
                        .GetResourceUrlPath(resource)
                }; ;
                yield return model;
            }
        }

        private static PathString GetServiceIndexUrlPath(
            this INugetServerProvider provider)
        {
            var majorVersion = provider
                .GetApiMajorVersionUrl();

            var path = provider
                ?.NugetServerOptions
                ?.ServiceIndex
                ?? throw new InvalidOperationException(
                    "Nuget server index not specified.");

            return majorVersion + path;
        }

        public static bool IsMatchServerIndex(
            this INugetServerProvider provider,
            HttpContext context)
        {
            if (!provider.IsMatchVerbs(context,
                HttpMethods.IsHead,
                HttpMethods.IsGet))
            {
                return false;
            }

            return provider.IsMatchPath(context,
                provider.GetServiceIndexUrlPath())
                && provider
                .IsMatchExtensionName(context, ".json");
        }
    }
}
