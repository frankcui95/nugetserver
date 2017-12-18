using MaiReo.Nuget.Server.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaiReo.Nuget.Server.Models
{
    public class PackageBaseAddressInputModel
    {
        public PackageBaseAddressInputModel(string baseUrl,
            PathString requestPath,
            PathString resourceUrlPath)
        {
            this.BaseUrl = baseUrl;
            this.RequestPath = requestPath;
            _splitedPath = requestPath
                .ToString()
                .ToLowerInvariant()
                .Split(new[] { '/' },
                StringSplitOptions.RemoveEmptyEntries);
            this.ResourceUrlPath = resourceUrlPath;
        }

        private readonly string[] _splitedPath;

        public string BaseUrl { get; }

        public PathString RequestPath { get; }

        public PathString ResourceUrlPath { get; }
        public string Id { get => GetId(); }

        public NuGetVersionString Version { get => GetVersion(); }

        private NuGetVersionString GetVersion()
        {
            if (_splitedPath.Length < 5)
            {
                return null;
            }
            if (NuGetVersionString.TryParse(_splitedPath[3], out var version))
            {
                return new NuGetVersionString(version);
            }
            return null;
        }
        private string GetId()
        {
            if (_splitedPath.Length < 3)
            {
                return string.Empty;
            }
            return _splitedPath[2].ToLowerInvariant();
        }

        public bool IsValid()
            => new Func<bool>[]
            {
                IsRequestingVersions,
                IsRequestingNupkg,
                IsRequestingNuspec
            }.Any(f => f());

        public bool IsRequestingVersions()
            => _splitedPath.Length == 4
            && string.Equals(_splitedPath[3], "index.json");

        public bool IsRequestingNupkg()
            => _splitedPath.Length == 5
            && Version != default(NuGetVersionString)
            && $"{_splitedPath[2]}.{_splitedPath[3]}.nupkg"
                == _splitedPath[4];

        public bool IsRequestingNuspec()
            => _splitedPath.Length == 5
            && Version != default(NuGetVersionString)
            && $"{_splitedPath[2]}.nuspec" == _splitedPath[4];


        public Uri GetRequestFileUri(Uri fsRootUri)
        {
            var pkgBaseUri = new Uri(BaseUrl +
                ResourceUrlPath + "/", UriKind.Absolute);
            var pathUri = new Uri(BaseUrl +
                RequestPath.ToUriComponent(), UriKind.Absolute);
            var requestFileUri = pkgBaseUri.MakeRelativeUri(pathUri);
            return new Uri(fsRootUri, requestFileUri);
        }
    }
}
