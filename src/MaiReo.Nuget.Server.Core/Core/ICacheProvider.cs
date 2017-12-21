using MaiReo.Nuget.Server.Events;
using MaiReo.Nuget.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Nuget.Server.Core
{
    public interface ICacheProvider
    {
        IEnumerable<Nuspec> GetAll();

        void OnAddPackageCompleted( object sender, AddPackageCompletedEventArgs eventArgs );

        void OnDeletePackageCompleted( object sender, DeletePackageCompletedEventArgs eventArgs );
    }
}
