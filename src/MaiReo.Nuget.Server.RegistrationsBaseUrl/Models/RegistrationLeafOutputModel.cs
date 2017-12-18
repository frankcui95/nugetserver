using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Nuget.Server.Models
{
    public class RegistrationLeafOutputModel
    {
        /// <summary>
        /// The URL to the registration leaf
        /// </summary>
        [JsonProperty("@id")]
        public string Id { get; set; }
        /// <summary>
        /// The catalog entry containing the package metadata
        /// </summary>
        public RegistrationCatalogEntryOutputModel CatalogEntry { get; set; }
        /// <summary>
        /// The URL to the package content (.nupkg)
        /// </summary>
        public string PackageContent { get; set; }

        public string Registration { get; set; }
    }
}
