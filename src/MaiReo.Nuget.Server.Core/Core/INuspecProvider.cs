using MaiReo.Nuget.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MaiReo.Nuget.Server.Core
{
    [EditorBrowsable( EditorBrowsableState.Never )]
    public interface INuspecProvider
    {
        IEnumerable<Nuspec> GetAll();
    }
}