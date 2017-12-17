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
    public static class NugetServerProviderExtensions
    {
        public static async Task RespondAsync(
            this INugetServerProvider provider,
            HttpContext context)
        {
            var serializer = provider
                .CreateJsonSerializerForServiceIndex();
            var baseUrl = context.GetBaseUrl();
            var apiMajorVersionUrl = provider.GetApiMajorVersionUrl();

            var serverIndex = new ServerIndexModel(
                provider.ParseResource(context))
            {
                Version = provider.NugetServerOptions.ApiVersion,
                Context = ServerIndexContext.Default
            };

            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, serverIndex);
            }

            context.Response.StatusCode
                = (int)System.Net.HttpStatusCode.OK;
            using (var content = new StringContent(
                sb.ToString(),
                Encoding.UTF8,
                "application/json"))
            {

                context.Response.ContentLength
                       = content.Headers.ContentLength;
                context.Response.ContentType
                    = content.Headers.ContentType.ToString();

                if (HttpMethods.IsHead(context.Request.Method))
                {
                    return;
                }

                if (HttpMethods.IsGet(context.Request.Method))
                {

                    await Task.Run(
                        () =>
                        content.CopyToAsync(
                            context.Response.Body),
                            context.RequestAborted);
                }
            }


        }

        public static JsonSerializer
        CreateJsonSerializerForServiceIndex(
            this INugetServerProvider provider)
        {
            var serializer = provider.CreateJsonSerializer();
            serializer.ContractResolver
                = new ServerIndexContractResolver();
            return serializer;
        }

        public static IEnumerable<ServerIndexResourceModel>
        ParseResource(this INugetServerProvider provider, HttpContext context)
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
                    Id = baseUrl + provider.GetResourceUrlPath(resource)
                }; ;
                yield return model;
            }
        }

        public static PathString GetServiceIndexUrlPath(
            this INugetServerProvider provider)
        {
            var majorVersion = provider.GetApiMajorVersionUrl();

            var path = provider
                ?.NugetServerOptions
                ?.ServiceIndex
                ?? throw new InvalidOperationException(
                    "Nuget server index not specified.");

            return majorVersion + path;
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
            
            return provider.IsMatchPath(provider.GetServiceIndexUrlPath(), context);
        }
    }
}
