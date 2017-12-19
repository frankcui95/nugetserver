using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Nuget.Server.Core
{
    public interface IPackageStatusProvider
    {
        bool IsDeleted( string id,
            string version );
    }

}
