using System;

namespace MaiReo.Nuget.Server.Events
{
    public class PackageEventArgs : EventArgs
    {
        public PackageEventArgs( string id,
            string version, string fileName )
        {
            Id = id;
            Version = version;
            this.FileName = fileName;
        }

        public virtual string Id { get; }

        public virtual string Version { get; }

        public virtual string FileName { get; }
    }
}