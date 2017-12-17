using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MaiReo.Nuget.Server.Core
{
    public class NugetServerOptions
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public const string DEFAULT_API_VERSION = "3.0.0-beta.1";

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const string DEFAULT_SERVICE_INDEX_V3 = "/index.json";

        [EditorBrowsable(EditorBrowsableState.Never)]
        public const string DEFAULT_Package_Directory = "packages";

        [EditorBrowsable(EditorBrowsableState.Never)]
        public NugetServerOptions()
        {
            ApiVersion = DEFAULT_API_VERSION;
            ServiceIndex = DEFAULT_SERVICE_INDEX_V3;
            PackageDirectory = DEFAULT_Package_Directory;
            Resources = new Dictionary<NugetServerResourceType, PathString>();
        }

        /// <summary>
        /// Gets or set nuget server api version. Only supports 3.0.0-beta.1 currently.
        /// </summary>
        public NuGetVersionString ApiVersion { get; set; }

        /// <summary>
        /// For more infomation, please visit https://docs.microsoft.com/en-us/nuget/api/service-index .
        /// </summary>
        public PathString ServiceIndex { get; set; }
        /// <summary>
        /// Gets or set nuget package (.nupkg) directory root path.
        /// Default is "packages".
        /// Case-(in)sensitive depends on platform.
        /// </summary>
        public string PackageDirectory { get; set; }

        /// <summary>
        /// For more infomation, please visit https://docs.microsoft.com/en-us/nuget/api/service-index .
        /// </summary>
        public IDictionary<NugetServerResourceType, PathString> Resources { get; private set; }
    }
}