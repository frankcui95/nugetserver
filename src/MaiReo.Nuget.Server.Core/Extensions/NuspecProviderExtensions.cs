using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MaiReo.Nuget.Server.Core
{
    public static class NuspecProviderExtensions
    {
        public static string GetPackageRootFullPath(
           this INuspecProvider provider)
        {
            var currentPath = Path.GetFullPath(".");

            var baseDir = AppDomain
                .CurrentDomain
                .BaseDirectory;

            if (string.IsNullOrWhiteSpace(
                provider
                ?.NugetServerOptions
                ?.PackageDirectory))
            {
                return currentPath ?? baseDir;
            }

            return Path.Combine(currentPath ?? baseDir,
                provider
                .NugetServerOptions
                .PackageDirectory);
        }

        public static IEnumerable<string> GetAllPackagePaths(
            this INuspecProvider provider,
            Func<string, bool> predicate = null)
            => Directory
            .EnumerateFiles(
                provider.GetPackageRootFullPath(),
                "*.nupkg",
                SearchOption.AllDirectories)
            .Where(predicate ?? True);

        private static bool True(string s) => true;

    }
}
