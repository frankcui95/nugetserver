using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaiReo.Nuget.Server.Core;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MaiReo.Nuget.Server.Models
{
    public class ServerIndexModel
    {
        public ServerIndexModel(IEnumerable<ServerIndexResourceModel> resources)
        {
            Resources = new HashSet<ServerIndexResourceModel>(
                resources,
                ServerIndexResourceModelComparer.Instance);
        }
        public string Version { get; set; }

        public IEnumerable<ServerIndexResourceModel> Resources { get; private set; }

        public ServerIndexContext Context { get; set; }

        

    }
}
