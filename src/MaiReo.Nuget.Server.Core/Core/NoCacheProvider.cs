using MaiReo.Nuget.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MaiReo.Nuget.Server.Events;

namespace MaiReo.Nuget.Server.Core
{
    public class NoCacheProvider : ICacheProvider
    {
        private readonly INuspecProvider _nuspecProvider;

        public NoCacheProvider( INuspecProvider nuspecProvider )
        {
            this._nuspecProvider = nuspecProvider;
        }

        public IEnumerable<Nuspec> GetAll()
        => _nuspecProvider.GetAll();
        

        public void OnAddPackageCompleted( object sender, AddPackageCompletedEventArgs eventArgs )
        {
        }

        public void OnDeletePackageCompleted( object sender, DeletePackageCompletedEventArgs eventArgs )
        {
        }
    }
}
