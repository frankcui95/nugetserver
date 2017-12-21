using System;
using System.Collections.Generic;
using System.Text;
using MaiReo.Nuget.Server.Models;

namespace MaiReo.Nuget.Server.Core
{
    public class CachedNuspecProvider : INuspecProvider
    {
        private readonly ICacheProvider _cacheProvider;

        public CachedNuspecProvider(
            ICacheProvider cacheProvider )
        {
            this._cacheProvider = cacheProvider;
        }

        public IEnumerable<Nuspec> GetAll() 
            => _cacheProvider.GetAll();
    }
}
