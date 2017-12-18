using MaiReo.Nuget.Server.Core;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
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
