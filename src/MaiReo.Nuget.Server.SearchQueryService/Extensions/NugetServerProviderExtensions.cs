using MaiReo.Nuget.Server.Core;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.SearchQueryService
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class
    NugetServerProviderExtensions
    {
        public static async Task RespondAsync(
            this INugetServerProvider provider,
            HttpContext context)
        {
            var serializer = provider.CreateJsonSerializer();
            var baseUrl = context.GetBaseUrl();
            PathString majorVersionUrl
                = provider.GetApiMajorVersionUrl();
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
                NugetServerResourceType.SearchQueryService,
                context);
        }

    }
}
