using System;

namespace MaiReo.Nuget.Server.Events
{
    public class AddPackageCompletedEventArgs : PackageEventArgs
    {
        public AddPackageCompletedEventArgs(
            string id, string version,
            string fileName ) : base( id, version, fileName )
        {

        }

    }
}