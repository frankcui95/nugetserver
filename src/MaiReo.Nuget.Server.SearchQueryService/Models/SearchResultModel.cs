using MaiReo.Nuget.Server.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaiReo.Nuget.Server.Models
{
    public class SearchResultModel
    {
        public SearchResultModel()
        {
            Id = string.Empty;
            Version = NuGetVersionString.Default;
            Description = string.Empty;
            Versions = new List<SearchResultPackageVersionModel>();
            Authors = new List<string>();
            IconUrl = string.Empty;
            LicenseUrl = string.Empty;
            Owners = new List<string>();
            ProjectUrl = string.Empty;
            Registration = string.Empty;
            Summary = string.Empty;
            Tags = new List<string>();
            Title = string.Empty;
        }
        /// <summary>
        /// The ID of the matched package
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The full SemVer 2.0.0 version string of the package (could contain build metadata)
        /// </summary>
        public NuGetVersionString Version { get; set; }

        public string Description { get; set; }
        /// <summary>
        /// All of the versions of the package matching the prerelease parameter
        /// </summary>
        public List<SearchResultPackageVersionModel> Versions { get; set; }

        public List<string> Authors { get; set; }

        public string IconUrl { get; set; }

        public string LicenseUrl { get; set; }

        public List<string> Owners { get; set; }

        public string ProjectUrl { get; set; }
        /// <summary>
        /// The absolute URL to the associated registration index
        /// </summary>
        public string Registration { get; set; }

        public string Summary { get; set; }

        public List<string> Tags { get; set; }

        public string Title { get; set; }
        /// <summary>
        /// This value can be inferred by the sum of downloads in the versions array
        /// </summary>
        public int TotalDownloads { get; set; }
        /// <summary>
        /// A JSON boolean indicating whether the package is verified
        /// </summary>
        public bool Verified { get; set; }

    }
}
