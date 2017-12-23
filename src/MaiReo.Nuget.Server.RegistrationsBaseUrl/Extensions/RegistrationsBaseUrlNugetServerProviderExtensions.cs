using MaiReo.Nuget.Server.Core;
using MaiReo.Nuget.Server.Models;
using Microsoft.AspNetCore.Http;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class RegistrationsBaseUrlNugetServerProviderExtensions
    {
        public static async Task RespondRegistrationsBaseUrlAsync(
            this INugetServerProvider provider,
            HttpContext context)
        {
            var excludeSemVer2Package = true;
            var useGzip = false;
            /*
             * Gzip does NOT work.
             */
            //if (provider.IsMatchResource(context,
            //    NugetServerResourceType.RegistrationsBaseUrl_3_6_0))
            //{
            //    useGzip = true;
            //    excludeSemVer2Package = false;
            //}
            //if (provider.IsMatchResource(context,
            //    NugetServerResourceType.RegistrationsBaseUrl_3_4_0))
            //{
            //    useGzip = true;
            //}
            var req = provider.GetRequestingMetadata(context);
            if (!req.IsValid())
            {
                provider.RespondNotFound(context);
                return;
            }
            var nuspecs = provider.NuspecProvider.GetAll()
            .Where(nuspec => nuspec.Metadata != null)
            .Where(n => n.Metadata.Id.ToLowerInvariant()
                    == req.Id)
            .Where(n => excludeSemVer2Package == false
                    || !((NuGetVersionString)n.Metadata.Version).IsSemVer2);
            if (!nuspecs.Any())
            {
                provider.RespondNotFound(context);
                return;
            }
            if (req.IsRequestingIndex())
            {
                await provider.RespondRegistrationIndexAsync(
                    context, req, nuspecs,
                    useGzip: useGzip);
                return;
            }
            if (req.IsRequestingPage())
            {
                //Do some stuff...
                return;
            }
            provider.RespondNotFound(context);
        }

        private static async Task RespondRegistrationIndexAsync(
            this INugetServerProvider provider,
            HttpContext context,
            RegistrationInputModel registrationInput,
            IEnumerable<Nuspec> nuspecs,
            bool useGzip = false)
        {
            var nuspecList = nuspecs
                .Select(x => new { Nuspec = x, Version = (NuGetVersionString)x.Metadata.Version })
                .OrderBy(x => x.Version)
                .Select(x => x.Nuspec)
                .ToList();

            var lowest = nuspecList.First().Metadata;
            var uppest = nuspecList.Last().Metadata;
            var parent = registrationInput.BaseUrl + registrationInput.Path;
            var lowestVersion = SemanticVersion.Parse(lowest.Version).ToFullString();
            var uppestVersion = SemanticVersion.Parse(uppest.Version).ToFullString();
            var packageContentBase = registrationInput.BaseUrl +
                provider.GetResourceUrlPath(NugetServerResourceType.PackageBaseAddress);
            var model = new RegistrationIndexOutputModel
            {
                Count = nuspecList.Count
            };

            foreach (var nuspec in nuspecList)
            {
                var publishTime = new DateTimeOffset(new FileInfo(nuspec.FilePath).LastWriteTimeUtc);
                var metadata = nuspec.Metadata;
                var modelItem = new RegistrationPageOutputModel()
                {
                    Id = $"{registrationInput.BaseUrl}{registrationInput.Path}#page/{metadata.Version}/{metadata.Version}",
                    Count = nuspecList.Count,
                    Lower = lowestVersion,
                    Upper = uppestVersion,
                };
                // If present Items, MUST also present Parent,Id need NOT be used.
                modelItem.Parent = parent;
                modelItem.Items = new List<RegistrationLeafOutputModel>()
                {
                    new RegistrationLeafOutputModel
                    {
                         Id =  registrationInput.BaseUrl +
                             registrationInput.PathBase +
                             $"/{metadata.Version}.json",
                         PackageContent =  packageContentBase +
                             $"/{metadata.Id.ToLowerInvariant()}" +
                             $"/{metadata.Version.ToLowerInvariant()}" +
                             $"/{metadata.Id.ToLowerInvariant()}" +
                             $".{metadata.Version.ToLowerInvariant()}.nupkg",
                         Registration = registrationInput.BaseUrl +
                            registrationInput.Path,
                         CatalogEntry = new RegistrationCatalogEntryOutputModel
                         {
                            Id_alias = registrationInput.BaseUrl +
                                registrationInput.PathBase +
                                $"/{metadata.Version}.json",
                            Id = metadata.Id,
                            Authors = metadata.Authors,
                            Description = metadata.Description,
                            ProjectUrl = metadata.ProjectUrl,
                            IconUrl = metadata.IconUrl,
                            Summary = metadata.ReleaseNotes ?? metadata.Description,
                            Tags = metadata.Tags?.Split(
                                new[] { ',' },
                                StringSplitOptions.RemoveEmptyEntries)
                            ?.ToList() ?? new List<string>(0),
                            Title = metadata.Id,
                            Version = metadata.Version,
                            LicenseUrl = metadata.LicenseUrl,
                            Listed = !provider.PackageStatusProvider.IsDeleted(metadata.Id,metadata.Version),
                            RequireLicenseAcceptance = metadata.RequireLicenseAcceptance,
                            PackageContent =  packageContentBase +
                             $"/{metadata.Id.ToLowerInvariant()}" +
                             $"/{metadata.Version.ToLowerInvariant()}" +
                             $"/{metadata.Id.ToLowerInvariant()}" +
                             $".{metadata.Version.ToLowerInvariant()}.nupkg",
                            Published = publishTime,
                            DependencyGroups = metadata.Dependencies
                                .Select(deps=>
                                new RegistrationDependencyGroupOutputModel
                                {
                                    //Id_alias = ""
                                    TargetFramework = deps.TargetFramework,
                                    Dependencies  = deps.Dependency.Select(dep=>
                                    new RegistrationDependencyOutputModel
                                    {
                                         Id = dep.Id,
                                         //Id_Alias = "",
                                         Range = dep.Version,
                                         Registration = registrationInput.BaseUrl +
                                             registrationInput.Path
                                    }).ToList()
                                }).ToList()

                         }
                    }
                };
                model.Items.Add(modelItem);
            }
            await provider.WriteJsonResponseAsync(context, model, useGzip: useGzip);
        }


        private static RegistrationInputModel GetRequestingMetadata(
            this INugetServerProvider provider,
            HttpContext context)
        {
            var model = new RegistrationInputModel(
                context.GetBaseUrl(),
                context.Request.Path);
            return model;
        }

        public static bool IsMatchRegistrationsBaseUrl(
            this INugetServerProvider provider,
            HttpContext context)
        {
            if (!provider.IsMatchVerbs(context,
                HttpMethods.IsHead,
                HttpMethods.IsGet))
            {
                return false;
            }
            return provider.IsMatchResources(
                context,
                NugetServerResourceType.RegistrationsBaseUrl,
                NugetServerResourceType.RegistrationsBaseUrl_3_0_0_beta,
                NugetServerResourceType.RegistrationsBaseUrl_3_0_0_rc
                /*
                 * Gzip does NOT work.
                 */
                //,
                //NugetServerResourceType.RegistrationsBaseUrl_3_4_0,
                //NugetServerResourceType.RegistrationsBaseUrl_3_6_0
                )
                && provider.IsMatchExtensionName(context, ".json");
        }
    }
}
