using MaiReo.Nuget.Server.Core;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.PackagePublish
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
            HttpContext context)
        {
            if (!provider.IsMatchVerbs(context,
                HttpMethods.IsPut,
                HttpMethods.IsDelete))
            {
                return false;
            }
            
            return provider.IsMatchResource(
                context,
                NugetServerResourceType.PackagePublish
                );
        }
        
    }
}
