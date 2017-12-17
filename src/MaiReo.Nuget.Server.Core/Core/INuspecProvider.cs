using MaiReo.Nuget.Server.Models;
using System.Collections.Generic;

namespace MaiReo.Nuget.Server.Core
{
    public interface INuspecProvider
    {
        NugetServerOptions NugetServerOptions { get; }
        IEnumerable<Nuspec> GetAll();
    }
}