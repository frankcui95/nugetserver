using MaiReo.Nuget.Server.Models;
using MaiReo.Nuget.Server.Tools;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MaiReo.Nuget.Server.Core
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ZipFileNuspecProvider : INuspecProvider
    {
        public ZipFileNuspecProvider(
            IOptions<NugetServerOptions> nugetServerOptionsAccessor)
        {
            this.NugetServerOptions = nugetServerOptionsAccessor.Value;
        }

        public NugetServerOptions NugetServerOptions { get; private set; }

        public virtual IEnumerable<Nuspec> GetAll(Func<string, bool> predicate = null)
        {
            foreach (var filePath in this.GetAllPackagePaths(predicate))
            {
                var nuspec = Zip.ReadNuspec(filePath);
                nuspec.FilePath = filePath;
                yield return nuspec;
            }
        }
    }
}
