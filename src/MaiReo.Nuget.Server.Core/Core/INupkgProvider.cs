using MaiReo.Nuget.Server.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MaiReo.Nuget.Server.Models;

namespace MaiReo.Nuget.Server.Core
{
    public interface INupkgProvider
    {
        IEnumerable<string> GetAll(
            Func<string, bool> predicate = null );

        Task<bool> AddAsync(
            NuspecMetadata metadata, Stream package,
            CancellationToken cancellationToken
            = default( CancellationToken ) );

        Task<bool> DeleteAsync( Nuspec nuspec,
            CancellationToken cancellationToken
            = default( CancellationToken ) );

    }
}
