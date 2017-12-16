using MaiReo.Nuget.Server.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace MaiReo.Nuget.Server.Core.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class NugetServerOptionsExtensions
    {
        public static PathString GetPackagePublishUrlPath(
            this NugetServerOptions options)
        {
            var majorVersion =
                options
                .GetApiMajorVersion()
                ?? throw new InvalidOperationException(
                    "Nuget server api version not specified.");

            var path =
                options
                .GetResourceValue(
                    NugetServerResourceTypes.PackagePublish);
            if (!path.HasValue)
            {
                throw new InvalidOperationException(
                    "Nuget server" +
                    NugetServerResourceTypes.PackagePublish +
                    " not specified.");
            }
            return $"/v{majorVersion}{path}";
        }

    }
}
