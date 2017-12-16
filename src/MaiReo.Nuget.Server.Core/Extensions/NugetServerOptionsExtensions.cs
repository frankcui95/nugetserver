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
        public static PathString GetResourceValue(
            this NugetServerOptions options,
            string type)
            =>
            options?.Resources?.ContainsKey(type) != true
            ? null
            : options.Resources[type];

        public static string GetApiMajorVersion(
            this NugetServerOptions options)
        {
            var majorVersion = new string(
                options
                ?.ApiVersion
                ?.TakeWhile(c => c != '-')
                ?.TakeWhile(c => c != '.')
                ?.ToArray()
                ?? new char[0]);

            if (string
                .IsNullOrWhiteSpace(majorVersion)) return null;

            if (majorVersion
                .Any(c => !char.IsDigit(c))) return null;

            return majorVersion;
        }

        public static string GetPackageRootFullPath(
            this NugetServerOptions options)
        {
            var baseDir = AppDomain
                .CurrentDomain
                .BaseDirectory;

            if (string
                .IsNullOrWhiteSpace(
                options?.PackageDirectory))
            {
                return baseDir;
            }

            return Path
                .Combine(baseDir,
                options.PackageDirectory);
        }

        public static IEnumerable<string> GetAllPackages(
            this NugetServerOptions options)
            => Directory
            .EnumerateFiles(
                options.GetPackageRootFullPath(),
                "*.nupkg",
                SearchOption.AllDirectories);
    }
}
