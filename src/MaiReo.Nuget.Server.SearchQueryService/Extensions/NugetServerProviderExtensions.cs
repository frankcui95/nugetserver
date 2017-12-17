using MaiReo.Nuget.Server.Core;
using MaiReo.Nuget.Server.Models;
using MaiReo.Nuget.Server.Tools;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.SearchQueryService
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class
    NugetServerProviderExtensions
    {
        public static async Task RespondAsync(
            this INugetServerProvider provider,
            HttpContext context)
        {
            var registrationsBaseUrl = context.GetBaseUrl() +
                provider.GetResourceUrlPath(
                    NugetServerResourceType.RegistrationsBaseUrl);

            var searchInputModel = context.FromQueryString<SearchInputModel>();
            var searchOutputModel = new SearchOutputModel();

            var metadata_versions =
                provider.NuspecProvider
                .GetAll()
                .Where(nuspec => nuspec.Metadata != null)
                .Select(nuspec => new
                {
                    nuspec.Metadata,
                    Version = (NuGetVersionString)nuspec.Metadata.Version
                })
                .Where(mv =>
                    searchInputModel.SemVerLevel?.Major != 1
                    ? true
                    : !mv.Version.IsSemVer2)
                .Where(mv =>
                    searchInputModel.PreRelease
                    ? true
                    : !mv.Version.IsPrerelease)
                .OrderBy(mv => mv.Metadata.Id)
                .GroupBy(mv => mv.Metadata.Id)
                .Where(mv => string.IsNullOrWhiteSpace(searchInputModel.Q)
                    || mv.Key.ToLowerInvariant().Contains(searchInputModel.Q.ToLowerInvariant()))
                .ToList();
            var takes = metadata_versions
                .Skip(searchInputModel.Skip)
                .Take(searchInputModel.Take)
                .ToList();

            searchOutputModel.TotalHits = metadata_versions.Count;

            foreach (var path_metadata in takes)
            {
                var packageId = path_metadata.Key;
                var packageIdLowerInvariant = packageId.ToLowerInvariant();

                var metadatas = path_metadata
                    .Select(x => x.Metadata)
                    .OrderBy(x => x.Version)
                    .ToList();

                var latest = metadatas.Last();
                searchOutputModel.Data.Add(new SearchResultModel
                {
                    Id = latest.Id,
                    Authors = latest.Authors?.Split(
                        new[] { ',' },
                        StringSplitOptions.RemoveEmptyEntries)
                        ?.ToList() ?? new List<string>(0),
                    Description = latest.Description,
                    Owners = latest.Owners?.Split(
                        new[] { ',' },
                        StringSplitOptions.RemoveEmptyEntries)
                        ?.ToList() ?? new List<string>(0),
                    ProjectUrl = latest.ProjectUrl,
                    IconUrl = latest.IconUrl
                    Registration = $"{registrationsBaseUrl}/{packageIdLowerInvariant}/index.json",
                    Summary = latest.ReleaseNotes,
                    Tags = latest.Tags?.Split(
                        new[] { ',' },
                        StringSplitOptions.RemoveEmptyEntries)
                        ?.ToList() ?? new List<string>(0),
                    Title = latest.Id,
                    Version = latest.Version,
                    LicenseUrl = latest.LicenseUrl,
                    Versions = metadatas.Select(m => new SearchResultPackageVersionModel
                    {
                        Id = $"{registrationsBaseUrl}/{packageIdLowerInvariant}/{m.Version}.json",
                        Version = m.Version
                    }).ToList(),
                });
            }
            await provider.WriteJsonResponseAsync(context, searchOutputModel);
        }

        public static bool IsMatch(
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
                NugetServerResourceType.SearchQueryService,
                NugetServerResourceType.SearchQueryService_3_0_0_beta,
                NugetServerResourceType.SearchQueryService_3_0_0_rc
                );
        }

    }
}
