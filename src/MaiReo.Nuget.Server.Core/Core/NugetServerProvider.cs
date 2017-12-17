using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace MaiReo.Nuget.Server.Core
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class NugetServerProvider : INugetServerProvider
    {
        public NugetServerProvider(
            IOptions<NugetServerOptions> nugetServerOptionsAccessor,
            IOptions<MvcJsonOptions> mvcJsonOptionsAccessor,
            INuspecProvider nuspecProvider)
        {
            this.NugetServerOptions = nugetServerOptionsAccessor.Value;
            this.MvcJsonOptions = mvcJsonOptionsAccessor.Value;
            this.NuspecProvider = nuspecProvider;
        }

        public NugetServerOptions NugetServerOptions { get; private set; }

        public MvcJsonOptions MvcJsonOptions { get; private set; }

        public INuspecProvider NuspecProvider { get; private set; }
    }
}
