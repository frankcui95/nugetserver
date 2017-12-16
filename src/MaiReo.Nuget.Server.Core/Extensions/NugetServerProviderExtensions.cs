using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;

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
                NullValueHandling = NullValueHandling.Ignore,
                Formatting =
#if DEBUG
                Formatting.Indented,
#else
                provider.MvcJsonOptions.SerializerSettings.Formatting,
#endif
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
    }
}
