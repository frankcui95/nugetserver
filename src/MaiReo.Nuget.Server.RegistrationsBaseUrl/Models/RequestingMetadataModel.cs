using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Nuget.Server.Models
{
    public class RequestingMetadataModel
    {
        public string Id { get; set; }
        public NuGetVersion Version { get; set; }
    }
}
