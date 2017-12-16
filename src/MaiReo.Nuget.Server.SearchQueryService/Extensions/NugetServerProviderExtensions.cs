using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.Core
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class NugetServerProviderExtensions
    {
        public static async Task RespondSearchQueryServiceAsync(
            this INugetServerProvider provider,
            HttpContext context)
        {
            var serializer = provider.CreateJsonSerializer();
            var baseUrl = context.GetBaseUrl();

        }

    }
}
