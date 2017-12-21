using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace MaiReo.Nuget.Server.Core
{
    [EditorBrowsable( EditorBrowsableState.Never )]
    public class NugetServerProvider : INugetServerProvider
    {
        public NugetServerProvider(
            IOptions<NugetServerOptions> nugetServerOptionsAccessor,
            IOptions<MvcJsonOptions> mvcJsonOptionsAccessor,
            INuspecProvider nuspecProvider,
            IPackageStatusProvider packageStatusProvider,
            INupkgProvider nupkgProvider,
            ICacheProvider cacheProvider)
        {
            this.NugetServerOptions = nugetServerOptionsAccessor.Value;
            this.MvcJsonOptions = mvcJsonOptionsAccessor.Value;
            this.NuspecProvider = nuspecProvider;
            this.PackageStatusProvider = packageStatusProvider;
            this.NupkgProvider = nupkgProvider;
            this.CacheProvider = cacheProvider;
        }

        public NugetServerOptions NugetServerOptions { get; }

        public MvcJsonOptions MvcJsonOptions { get;  }

        public INuspecProvider NuspecProvider { get; }
        public IPackageStatusProvider PackageStatusProvider { get; }

        public INupkgProvider NupkgProvider { get; }
        public ICacheProvider CacheProvider { get; }
    }
}
