using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.ComponentModel;
using System.Linq;

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

        public static bool IsMatchVerbs(
            this INugetServerProvider provider,
            HttpContext context,
            params Func<string, bool>[] verbs)
            => verbs?.Any(
                v => v?.Invoke(context.Request.Method) == true) == true;

        public static bool IsMatchResource(
            this INugetServerProvider provider,
            NugetServerResourceType resourceType,
            HttpContext context)
            => context
                .IsRequestingUrl(provider
                .GetResourceUrlPath(resourceType));

        public static bool IsMatchPath(
            this INugetServerProvider provider,
            PathString path,
            HttpContext context)
            => context
                .IsRequestingUrl(path);
    }
}
