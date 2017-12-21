using System;
using System.Collections.Generic;
using MaiReo.Nuget.Server.Core;
using MaiReo.Nuget.Server.Models;
using System.Collections.Concurrent;
using System.Linq;
using MaiReo.Nuget.Server.Events;
using MaiReo.Nuget.Server.Tools;

namespace Microsoft.Extensions.DependencyInjection
{
    public class InMemoryCacheProvider : ICacheProvider
    {
        private static readonly Func<string, bool> True;
        private readonly ZipFileNuspecProvider _zipFileNuspecProvider;
        static InMemoryCacheProvider()
        {
            True = _ => true;
        }

        public InMemoryCacheProvider(
            ZipFileNuspecProvider zipFileNuspecProvider )
        {
            CachedNuspec = new ConcurrentDictionary<string, ConcurrentDictionary<string, Nuspec>>();
            this._zipFileNuspecProvider = zipFileNuspecProvider;
            var nuspecs = this._zipFileNuspecProvider.GetAll().Where( n => n?.Metadata != null );
            var idGroups = nuspecs.GroupBy( n => n.Metadata.Id );
            foreach (var idGroup in idGroups)
            {
                var versionDic = idGroup.Select( x => x ).ToDictionary( x => x.Metadata.Version );
                var concurrentDic = new ConcurrentDictionary<string, Nuspec>( versionDic );
                CachedNuspec.TryAdd( idGroup.Key, concurrentDic );
            }
        }

        public IEnumerable<Nuspec> GetAll()
            => CachedNuspec
                .SelectMany( id =>
                    id.Value.Select( v => v.Value ) );

        public void OnAddPackageCompleted( object sender,
            AddPackageCompletedEventArgs eventArgs )
            => UpdateCache( sender as INupkgProvider, eventArgs );

        public void OnDeletePackageCompleted( object sender,
            DeletePackageCompletedEventArgs eventArgs )
            => UpdateCache( sender as INupkgProvider, eventArgs );

        private void UpdateCache( INupkgProvider nupkgProvider, AddPackageCompletedEventArgs eventArgs )
        {
            lock (CachedNuspec)
            {
                var nuspec = Zip.ReadNuspec( eventArgs.FileName );
                if (nuspec == null)
                {
                    return;
                }
                nuspec.FilePath = eventArgs.FileName;
                var versionDic = CachedNuspec.GetOrAdd( eventArgs.Id
                    , id => new ConcurrentDictionary<string, Nuspec>() );
                versionDic.AddOrUpdate( eventArgs.Version,
                    nuspec, ( version, old ) => nuspec );
            }
        }
        private void UpdateCache( INupkgProvider nupkgProvider, DeletePackageCompletedEventArgs eventArgs )
        {
            lock (CachedNuspec)
            {
                var versionDic = CachedNuspec.GetOrAdd( eventArgs.Id
                    , id => new ConcurrentDictionary<string, Nuspec>() );
                versionDic.TryRemove( eventArgs.Version, out var removed );
            }
        }

        protected virtual ConcurrentDictionary<string, ConcurrentDictionary<string, Nuspec>> CachedNuspec { get; set; }



    }
}