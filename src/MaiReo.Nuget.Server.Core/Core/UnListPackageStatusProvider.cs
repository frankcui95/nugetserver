using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Nuget.Server.Core
{
    public class UnListPackageStatusProvider : IPackageStatusProvider
    {
        public NugetServerOptions NugetServerOptions { get; }

        public UnListPackageStatusProvider(
            IOptions<NugetServerOptions> nugetServerOptionsAccessor )
        {
            this.NugetServerOptions = nugetServerOptionsAccessor.Value;
        }

        public virtual bool IsDeleted( string id, string version )
        {
            var unlistEnabled = NugetServerOptions
                ?.InterpretUnListEnabled == true;
            if (!unlistEnabled) return false;
            throw new NotImplementedException( "This method must be implemented by derived class." );
        }
    }
}
