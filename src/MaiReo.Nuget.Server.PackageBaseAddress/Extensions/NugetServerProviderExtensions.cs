using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.Core
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class NugetServerProviderExtensions
    {
        public static async Task RespondPackageBaseAddressAsync(
            this INugetServerProvider provider,
            HttpContext context)
        {
           
        }

    }
}
