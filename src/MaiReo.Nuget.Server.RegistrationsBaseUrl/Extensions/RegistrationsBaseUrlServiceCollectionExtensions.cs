using MaiReo.Nuget.Server;
using MaiReo.Nuget.Server.Middlewares;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegistrationsBaseUrlServiceCollectionExtensions
    {
        public static IServiceCollection
        AddNugetServerRegistrationsBaseUrl(
            this IServiceCollection services)
        {
            services.AddNugetServerCore(
                opt => 
                {
                    opt.Resources.Add(
                        NugetServerResourceType
                        .RegistrationsBaseUrl,
                        "/registration3");
                    opt.Resources.Add(
                       NugetServerResourceType
                       .RegistrationsBaseUrl_3_0_0_beta,
                       "/registration3");
                    opt.Resources.Add(
                       NugetServerResourceType
                       .RegistrationsBaseUrl_3_0_0_rc,
                       "/registration3");
                    /*
                     * Gzip does NOT work.
                     */
                    //opt.Resources.Add(
                    //    NugetServerResourceType
                    //    .RegistrationsBaseUrl_3_4_0,
                    //    "/registration3-gz");

                    //opt.Resources.Add(
                    //    NugetServerResourceType
                    //    .RegistrationsBaseUrl_3_6_0,
                    //    "/registration3-gz-semver2");
                    
                })
            .TryAddTransient<RegistrationsBaseUrlMiddleware>();

            return services;
        }
    }
}
