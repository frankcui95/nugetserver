using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Headers;
using MaiReo.Nuget.Server.Core;

namespace MaiReo.Nuget.Server.Models
{
    public class PackageInputModel
    {
        public const string X_NUGET_APIKEY_HEADER_NAME = "X-NuGet-ApiKey";
        public static readonly MediaTypeHeaderValue PushContentType;

        //Push response
        //201,202 The package was successfully pushed
        //400 The provided package is invalid
        //409 A package with the provided ID and version already exists

        //Delete response
        //204 The package was deleted
        //404 No package with the provided ID and VERSION exists

        static PackageInputModel()
        {
            PushContentType =
                new MediaTypeHeaderValue( "multipart/form-data" );
        }

        public PackageInputModel(
            string requestMethod,
            RequestHeaders requestHeaders,
            PathString requestPath )
        {
            _requestHeaders = requestHeaders;
            _splitedPath = requestPath
                .ToString()
                .ToLowerInvariant()
                .Split( new[] { '/' },
                StringSplitOptions.RemoveEmptyEntries );
            Method = requestMethod;
            ApiKey = _requestHeaders
                ?.Headers[X_NUGET_APIKEY_HEADER_NAME]
                .FirstOrDefault();
            Path = requestPath;
        }


        private readonly RequestHeaders _requestHeaders;
        private readonly string[] _splitedPath;

        public string Method { get; }

        public PathString Path { get; }

        public string ApiKey { get; }
        public string Id => GetId();



        public NuGetVersionString Version => GetVersion();

        public string GetId()
        {
            if (_splitedPath.Length < 3) return null;
            return _splitedPath[2];
        }

        public NuGetVersionString GetVersion()
        {
            if (_splitedPath.Length < 4) return null;
            try
            {
                return (NuGetVersionString)_splitedPath[3];
            }
            catch
            {
            }
            return null;
        }

        public bool IsValid()
            =>
            (!string.IsNullOrWhiteSpace( ApiKey ))
            && new Func<bool>[]
            {
                IsPush,
                IsDelete
            }.Any( f => f() );

        public bool IsPush()
        {
            return HttpMethods
                    .IsPut( Method )
                   && _requestHeaders.ContentType != null
                   && _requestHeaders.ContentType
                       .IsSubsetOf( PushContentType );
    }
        public bool IsDelete()
        {
            return HttpMethods.IsDelete( Method )
                 && Id != null
                 && Version != null;

        }
    }
}