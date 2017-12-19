using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Nuget.Server.Core
{
    public class NullUnListPackageStatusProvider
        : UnListPackageStatusProvider, IPackageStatusProvider
    {
        public NullUnListPackageStatusProvider(
            IOptions<NugetServerOptions> nugetServerOptionsAccessor )
            : base( nugetServerOptionsAccessor )
        {
        }

        public override bool IsDeleted( string id, string version )
        {
            return false;
        }
    }
}
