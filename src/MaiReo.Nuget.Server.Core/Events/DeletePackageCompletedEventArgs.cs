using System;

namespace MaiReo.Nuget.Server.Events
{
    public class DeletePackageCompletedEventArgs : PackageEventArgs
    {
        public DeletePackageCompletedEventArgs(
            string id, string version,
            string fileName ) : base( id, version, fileName )
        {

        }

    }
}