using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;
using MaiReo.Nuget.Server.Models;
using System.Xml;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.Tools
{
    public static class Zip
    {


        public static Nuspec ReadNuspec( string fileName )
        {
            try
            {
                using (var arc = ZipFile.OpenRead( fileName ))
                {
                    var nuspecEntry = arc.Entries
                        .Where( e => e.FullName == e.Name )
                        .FirstOrDefault( e => e.Name.EndsWith( ".nuspec" ) );
                    if (nuspecEntry == null) return default( Nuspec );
                    using (var zipStream = nuspecEntry.Open())
                    {
                        Nuspec.TryParse( zipStream, out var nuspec );
                        return nuspec;
                    }
                }

            }
            catch
            {
            }
            return default( Nuspec );
        }

        public static async Task<byte[]>
        ReadNuspecRawAsync( Nuspec nuspec )
        {
            if (nuspec == null)
                return null;
            try
            {
                using (var arc =
                    ZipFile.OpenRead( nuspec.FilePath ))
                {
                    var nuspecEntry = arc.Entries
                        .Where( e =>
                            e.FullName == e.Name )
                        .FirstOrDefault( e =>
                            e.Name.EndsWith( ".nuspec" ) );
                    if (nuspecEntry == null)
                        return null;

                    using (var ms = new MemoryStream())
                    using (var zipStream = nuspecEntry.Open())
                    {
                        await zipStream.CopyToAsync( ms );
                        return ms.ToArray();
                    }

                }
            }
            catch
            {
            }
            return null;
        }

        public static async Task<Nuspec>
        ReadNuspecFromPackageAsync( Stream package )
        {
            var nuspecRaw = await ReadNuspecRawFromPackageAsync( package );
            Nuspec.TryParse( nuspecRaw, out var nuspec );
            return nuspec;
        }


        public static async Task<byte[]>
        ReadNuspecRawFromPackageAsync( byte[] package )
        {
            if (package == null)
                return null;
            try
            {
                using (var inputStream
                    = new MemoryStream( package ))
                {
                    return await
                        ReadNuspecRawFromPackageAsync(
                            inputStream );
                }
            }
            catch
            {
            }
            return null;
        }

        public static async Task<byte[]>
        ReadNuspecRawFromPackageAsync( Stream package, bool leaveOpen = true )
        {
            if (package == null)
                return null;
            if (!package.CanRead)
                return null;

            try
            {
                using (var arc = new ZipArchive(
                    package, ZipArchiveMode.Read, leaveOpen ))
                {
                    var nuspecEntry = arc.Entries
                        .Where( e =>
                            e.FullName == e.Name )
                        .FirstOrDefault( e =>
                            e.Name.EndsWith( ".nuspec" ) );
                    if (nuspecEntry == null)
                        return null;

                    using (var outputStream = new MemoryStream())
                    using (var zipStream = nuspecEntry.Open())
                    {
                        await zipStream
                            .CopyToAsync( outputStream );
                        return outputStream.ToArray();
                    }
                }
            }
            catch
            {
            }
            return null;
        }


    }
}
