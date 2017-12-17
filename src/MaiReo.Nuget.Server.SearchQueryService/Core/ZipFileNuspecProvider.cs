using System;
using System.Collections.Generic;
using System.Text;
using MaiReo.Nuget.Server.Models;
using MaiReo.Nuget.Server.Tools;
using Microsoft.Extensions.Options;

namespace MaiReo.Nuget.Server.Core
{
    public class ZipFileNuspecProvider : INuspecProvider
    {
        public ZipFileNuspecProvider(
            IOptions<NugetServerOptions> nugetServerOptionsAccessor)
        {
            this.NugetServerOptions = nugetServerOptionsAccessor.Value;
        }

        public NugetServerOptions NugetServerOptions { get; private set; }

        public IEnumerable<Nuspec> GetAll()
        {
            foreach (var filePath in this.GetAllPackagePaths())
            {
                var nuspec = Zip.ReadNuspec(filePath);
                nuspec.FilePath = filePath;
                yield return nuspec;
            }
        }
    }
}
