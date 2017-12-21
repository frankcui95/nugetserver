using MaiReo.Nuget.Server.Models;
using MaiReo.Nuget.Server.Tools;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MaiReo.Nuget.Server.Core
{
    [EditorBrowsable( EditorBrowsableState.Never )]
    public class ZipFileNuspecProvider : INuspecProvider
    {
        public ZipFileNuspecProvider( INupkgProvider nupkgProvider )
        {
            this._nupkgProvider = nupkgProvider;
        }

        private readonly INupkgProvider _nupkgProvider;

        public virtual IEnumerable<Nuspec> GetAll()
        {
            foreach (var filePath in _nupkgProvider.GetAll())
            {
                var nuspec = Zip.ReadNuspec( filePath );
                nuspec.FilePath = filePath;
                yield return nuspec;
            }
        }
    }
}
