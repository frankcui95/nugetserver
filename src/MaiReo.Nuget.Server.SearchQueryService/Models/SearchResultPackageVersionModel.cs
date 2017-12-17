using MaiReo.Nuget.Server.Core;
using Newtonsoft.Json;

namespace MaiReo.Nuget.Server.Models
{
    public class SearchResultPackageVersionModel
    {
        public SearchResultPackageVersionModel()
        {
            Id = string.Empty;
            Version = NuGetVersionString.Default;
        }

        [JsonProperty("@id")]
        public string Id { get; set; }

        public NuGetVersionString Version { get; set; }

        public int Downloads { get; set; }
    }
}