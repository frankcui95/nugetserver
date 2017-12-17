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
                    NugetServerResourceType.RegistrationsBaseUrl_3_4_0);

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
                    searchInputModel.SemVerLevel?.IsSemVer2 == true
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
                    Registration = $"{registrationsBaseUrl}/{packageIdLowerInvariant}/index.json",
                    Title = latest.Id,
                    Version = latest.Version,
                    Versions = metadatas.Select(m => new SearchResultPackageVersionModel
                    {
                        Id = $"{registrationsBaseUrl}/{packageIdLowerInvariant}/{m.Version}.json",
                        Version = m.Version
                    }).ToList(),
                });
            }
            var serializer = provider.CreateJsonSerializer();
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, searchOutputModel);
            }

            context.Response.StatusCode
                = (int)System.Net.HttpStatusCode.OK;
            using (var content = new StringContent(
                sb.ToString(),
                Encoding.UTF8,
                "application/json"))
            {

                context.Response.ContentLength
                       = content.Headers.ContentLength;
                context.Response.ContentType
                    = content.Headers.ContentType.ToString();

                if (HttpMethods.IsHead(context.Request.Method))
                {
                    return;
                }

                if (HttpMethods.IsGet(context.Request.Method))
                {

                    await Task.Run(
                        () =>
                        content.CopyToAsync(
                            context.Response.Body),
                            context.RequestAborted);
                }

            }
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

            return provider.IsMatchResource(
                NugetServerResourceType.SearchQueryService,
                context);
        }

    }
}
