using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using MaiReo.Nuget.Server.Events;
using System.Threading.Tasks;
using System.Threading;
using MaiReo.Nuget.Server.Models;

namespace MaiReo.Nuget.Server.Core
{
    [EditorBrowsable( EditorBrowsableState.Never )]
    public class NupkgProvider : INupkgProvider
    {
        public NupkgProvider(
            IOptions<NugetServerOptions> optionsAccessor )
        {
            this.NugetServerOptions = optionsAccessor.Value;
        }

        public virtual NugetServerOptions NugetServerOptions { get; }

        public virtual async Task<bool> AddAsync(
            NuspecMetadata metadata, Stream package,
            CancellationToken cancellationToken
            = default( CancellationToken ) )
        {
            var id = metadata.Id;
            var version = metadata.Version;

            var fileName = CombineToFilePath( id, version );
            var result = await AddPackageAsync(
                fileName, package, cancellationToken );


            if (result.Item1)
            {
                await NugetServerOptions
                    ?.InvokeAddPackageCompletedAsync( this,
                    new AddPackageCompletedEventArgs(
                         id, version, fileName
                        ) );
            }
            else
            {
                await NugetServerOptions
                   ?.InvokeAddPackageFailedAsync( this,
                   new AddPackageFailedEventArgs(
                        id, version, fileName, result.Item2
                       ) );
            }


            return result.Item1;
        }

        public virtual async Task<bool> DeleteAsync(
            Nuspec nuspec,
            CancellationToken cancellationToken
            = default( CancellationToken ) )
        {
            var result = DeletePackage( nuspec.FilePath );
            if (result.Item1)
            {
                await NugetServerOptions
                    ?.InvokeDeletePackageCompletedAsync( this,
                    new DeletePackageCompletedEventArgs(
                        nuspec.Metadata.Id,
                        nuspec.Metadata.Version,
                        nuspec.FilePath
                        ) );
            }
            else
            {
                await NugetServerOptions
                   ?.InvokeDeletePackageFailedAsync( this,
                   new DeletePackageFailedEventArgs(
                        nuspec.Metadata.Id,
                        nuspec.Metadata.Version,
                        nuspec.FilePath,
                        result.Item2
                       ) );
            }
            return result.Item1;
        }

        private async Task<Tuple<bool, Exception>> AddPackageAsync(
            string fileName, Stream package,
            CancellationToken cancellationToken )
        {
            if (fileName == null)
            {
                throw new ArgumentNullException( nameof( fileName ) );
            }
            if (package == null)
            {
                throw new ArgumentNullException( nameof( package ) );
            }
            var fileInfo = new FileInfo( fileName );
            
            if (fileInfo.Exists)
                return Tuple.Create( false,
                    (Exception)new InvalidOperationException(
                        "Package file exists." ) );
            
            var exception = default( Exception );
            try
            {
                if (!fileInfo.Directory.Exists)
                {
                    fileInfo.Directory.Create();
                }
                using (var fs = fileInfo.OpenWrite())
                {
                    if (!fs.CanWrite)
                        return Tuple.Create( false,
                           (Exception)
                           new InvalidOperationException(
                               "Package file cannot write." ) );
                    try
                    {
                        await package.CopyToAsync( fs,
                            4096, cancellationToken );
                        
                        return Tuple.Create(
                            true, default( Exception ) );
                    }
                    catch (TaskCanceledException)
                    {
                        fileInfo.Delete();
                        throw;
                    }
                }
            }

            catch (Exception ex)
            {
                exception = ex;
            }
            return Tuple.Create(
                    false, exception );
        }

        private Tuple<bool, Exception> DeletePackage( string fileName )
        {
            var exception = default( Exception );
            var fileInfo = new FileInfo( fileName );
            if (!fileInfo.Exists)
                return Tuple.Create( false,
                    (Exception)
                    new FileNotFoundException(
                        "Package not found.", fileName ) );
            try
            {
                fileInfo.Delete();
                return Tuple.Create( true, default( Exception ) );
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return Tuple.Create( false, exception );
        }

        public virtual IEnumerable<string> GetAll(
            Func<string, bool> predicate = null )
            => Directory
            .EnumerateFiles(
                this.GetPackageRootFullPath(),
                "*.nupkg",
                SearchOption.AllDirectories )
            .Where( predicate ?? True );

        private string CombineToFilePath(
            string id, string version )
        {
            var lowerId = id.ToLowerInvariant();
            var lowerVersion = version
                .ToLowerInvariant();
            return Path.Combine(
                this.GetPackageRootFullPath(),
                lowerId,
                lowerVersion,
                $"{lowerId}.{lowerVersion}.nupkg"
            );
        }

        private string GetPackageRootFullPath()
        {
            var currentPath = Path.GetFullPath( "." );

            var baseDir = AppDomain
                .CurrentDomain
                .BaseDirectory;

            if (string.IsNullOrWhiteSpace(
                NugetServerOptions
                ?.PackageDirectory ))
            {
                return currentPath ?? baseDir;
            }

            return Path.Combine( currentPath ?? baseDir,
                NugetServerOptions
                .PackageDirectory );
        }



        private static bool True( string s ) => true;
    }
}
