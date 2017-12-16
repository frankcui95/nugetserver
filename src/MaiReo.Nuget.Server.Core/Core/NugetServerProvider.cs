using MaiReo.Nuget.Server.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Nuget.Server.Core
{
    public class NugetServerProvider : INugetServerProvider
    {
        public NugetServerProvider(
            IOptions<NugetServerOptions> nugetServerOptionsAccessor,
            IOptions<MvcJsonOptions> mvcJsonOptionsAccessor)
        {
            this.NugetServerOptions = nugetServerOptionsAccessor.Value;
            this.MvcJsonOptions = mvcJsonOptionsAccessor.Value;
        }

        public NugetServerOptions NugetServerOptions { get; private set; }

        public MvcJsonOptions MvcJsonOptions { get; private set; }

    }
}
