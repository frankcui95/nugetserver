using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.Core
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class NugetServerProviderExtensions
    {
        public static JsonSerializer CreateJsonSerializer(
            this INugetServerProvider provider)
        {
            return new JsonSerializer
            {
                NullValueHandling
                = NullValueHandling.Ignore,
                Formatting =
#if DEBUG
                Formatting.Indented,
#else
                provider
                .MvcJsonOptions
                .SerializerSettings.Formatting,
#endif
                ContractResolver
                = new CamelCasePropertyNamesContractResolver()
            };
        }

        public static async Task WriteJsonResponseAsync<T>(
            this INugetServerProvider provider,
            HttpContext context, T value,
            JsonSerializer serializer = null)
            where T : class
        {
            serializer = serializer ?? CreateJsonSerializer(provider);

            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, value);
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

        public static PathString GetApiMajorVersionUrl(
            this INugetServerProvider provider)
        {
            var majorVersion = provider
                ?.NugetServerOptions
                ?.ApiVersion?.Major;

            if (!majorVersion.HasValue)
            {
                throw new InvalidOperationException(
                    "Nuget server api version not specified.");
            }
            return "/v" + majorVersion;

        }
        public static PathString GetResourceValue(
            this INugetServerProvider provider,
            NugetServerResourceType resourceType)
            =>
            provider
            ?.NugetServerOptions
            ?.Resources
            ?.ContainsKey(resourceType) != true
            ? null
            : provider
            .NugetServerOptions
            .Resources[resourceType];


        public static PathString GetResourceUrlPath(
            this INugetServerProvider provider,
            NugetServerResourceType resourceType)
        {
            var majorVersion = provider
                .GetApiMajorVersionUrl();

            var path = provider.GetResourceValue(
                    resourceType);
            if (!path.HasValue)
            {
                throw new InvalidOperationException(
                    "Nuget server resource " +
                    resourceType +
                    " not specified.");
            }
            return majorVersion + path;
        }
        public static IEnumerable<PathString> GetResourceUrlPaths(
            this INugetServerProvider provider,
            params NugetServerResourceType[] resourceTypes) =>
            resourceTypes
            ?.Select(t => GetResourceUrlPath(provider, t))
            ?? Enumerable.Empty<PathString>();

        public static bool IsMatchVerbs(
            this INugetServerProvider provider,
            HttpContext context,
            params Func<string, bool>[] verbs)
            => verbs?.Any(
                v => v?.Invoke(context.Request.Method) == true) == true;

        public static bool IsMatchResources(
            this INugetServerProvider provider,
            HttpContext context,
            params NugetServerResourceType[] resourceTypes
            )
            => context
                .IsRequestingUrls(provider
                .GetResourceUrlPaths(resourceTypes));

        public static bool IsMatchResource(
            this INugetServerProvider provider,
            HttpContext context,
            NugetServerResourceType resourceType
            )
            => context
                .IsRequestingUrl(provider
                .GetResourceUrlPath(resourceType));

        public static bool IsMatchPath(
            this INugetServerProvider provider,
            HttpContext context,
            PathString path
            )
            => context
                .IsRequestingUrl(path);
    }
}
