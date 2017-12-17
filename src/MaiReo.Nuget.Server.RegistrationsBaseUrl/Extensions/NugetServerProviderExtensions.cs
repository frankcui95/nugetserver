using MaiReo.Nuget.Server.Core;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
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
            var serializer = provider.CreateJsonSerializer();
            var baseUrl = context.GetBaseUrl();
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
                NugetServerResourceType.RegistrationsBaseUrl_3_0_0_rc,
                NugetServerResourceType.RegistrationsBaseUrl_3_4_0,
                NugetServerResourceType.RegistrationsBaseUrl_3_6_0
                );
        }
    }
}
