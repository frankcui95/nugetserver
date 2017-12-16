using MaiReo.Nuget.Server.Configurations;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace MaiReo.Nuget.Server.Core
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface INugetServerProvider
    {
        NugetServerOptions NugetServerOptions { get; }

        MvcJsonOptions MvcJsonOptions { get; }
    }
}
