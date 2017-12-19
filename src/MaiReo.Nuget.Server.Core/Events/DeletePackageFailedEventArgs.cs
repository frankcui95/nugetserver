using System;

namespace MaiReo.Nuget.Server.Events
{
    public class DeletePackageFailedEventArgs : PackageEventArgs , IHandleableEventArgs
    {
        public DeletePackageFailedEventArgs(
            string id, string version, string fileName,
            Exception exception
            ) : base( id, version, fileName )
        {
            this.Exception = exception;
        }

        public virtual Exception Exception { get; }

        public virtual bool IsHandled { get; set; }
    }
}