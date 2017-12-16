using MaiReo.Nuget.Server.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace MaiReo.Nuget.Server.Configurations.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class NugetServerOptionsExtensions
    {
        public static PathString GetServiceIndexUrlPath(
            this NugetServerOptions options)
        {
            var majorVersion =
                options
                .GetApiMajorVersion()
                ?? throw new InvalidOperationException(
                    "Nuget server api version not specified.");
            var path =
                options
                ?.ServiceIndex
                ?? throw new InvalidOperationException(
                    "Nuget server index not specified.");
               
            return $"/v{majorVersion}{path}";
        }
        

    }
}
