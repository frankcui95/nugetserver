using MaiReo.Nuget.Server.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MaiReo.Nuget.Server.Serialization
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ServerIndexNamingStrategy : CamelCaseNamingStrategy
    {
        static ServerIndexNamingStrategy()
        {
            PropertyNameNeedPrefix = new[]
            {
                nameof(ServerIndex.Context),
                nameof(ServerIndexContext.Vocab),
                nameof(ServerIndexResource.Id),
                nameof(ServerIndexResource.Type),
            };
        }
        public static string[] PropertyNameNeedPrefix { get; private set; }

        public const string PropertyNamePrefix = @"@";

        protected override string ResolvePropertyName(
            string name)
        {
            var resolved = base.ResolvePropertyName(name);
            if (PropertyNameNeedPrefix.Any(p => p == name))
            {
                return PropertyNamePrefix + resolved;
            }
            return resolved;
        }

    }
}
