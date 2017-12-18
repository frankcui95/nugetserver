using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MaiReo.Nuget.Server.Models
{
    public class RegistrationCatalogEntryOutputModel
    {
        public RegistrationCatalogEntryOutputModel()
        {
            Language = string.Empty;
            IconUrl = string.Empty;
            Summary = string.Empty;
            Tags = new List<string>(1)
            {
                string.Empty
            };
            Title = string.Empty;
            Listed = true;
            Published = DateTimeOffset.UtcNow;
            MinClientVersion = "2.6";
            DependencyGroups =
                new List<RegistrationDependencyGroupOutputModel>(0);
        }

        [JsonProperty("@id")]
        public string Id_alias { get; set; }

        public string Authors { get; set; }

        public List<RegistrationDependencyGroupOutputModel>
            DependencyGroups { get; set; }

        public string Description { get; set; }

        public string IconUrl { get; set; }

        public string Id { get; set; }

        public string Language { get; set; }

        public string LicenseUrl { get; set; }

        public bool Listed { get; set; }

        public string MinClientVersion { get; set; }

        public string PackageContent { get; set; }

        public string ProjectUrl { get; set; }

        public DateTimeOffset Published { get; set; }

        public bool RequireLicenseAcceptance { get; set; }

        public string Summary { get; set; }

        public List<string> Tags { get; set; }

        public string Title { get; set; }

        public string Version { get; set; }

    }
}