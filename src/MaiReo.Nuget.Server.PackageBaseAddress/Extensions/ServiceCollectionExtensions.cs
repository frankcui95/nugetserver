using MaiReo.Nuget.Server;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection
        AddNugetServerPackageBaseAddress(
            this IServiceCollection services,
            string url = null) 
            => 
            services
            .AddNugetServerCore(
                opt => 
                opt
                .Resources
                .Add(
                   NugetServerResourceTypes.PackageBaseAddress,
                    url ?? "flatcontainer"));
    }
}
