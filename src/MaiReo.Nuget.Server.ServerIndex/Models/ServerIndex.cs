using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaiReo.Nuget.Server.Configurations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MaiReo.Nuget.Server.Models
{
    public class ServerIndex
    {
        private readonly string _baseUrl;
        public ServerIndex(string baseUrl, NugetServerOptions options)
        {
            this._baseUrl = baseUrl;
            var resources = ParseResource(options?.Resources);
            Resources = new HashSet<ServerIndexResource>(
                resources,
                ServerIndexResourceComparer.Instance);
        }
        public string Version { get; set; }

        public IEnumerable<ServerIndexResource> Resources { get; private set; }

        private IEnumerable<ServerIndexResource> ParseResource(IEnumerable<KeyValuePair<string, PathString>> resources)
        {
            if (resources == null)
            {
                yield break;
            }
            foreach (var resource in resources)
            {
                yield return new ServerIndexResource
                {
                    Type = resource.Key,
                    Id = _baseUrl + resource.Value
                };
            }
        }
    }
}
