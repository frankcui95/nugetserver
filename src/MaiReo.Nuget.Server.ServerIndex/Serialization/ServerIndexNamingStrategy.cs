using MaiReo.Nuget.Server.Models;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.Linq;

namespace MaiReo.Nuget.Server.Serialization
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ServerIndexNamingStrategy : CamelCaseNamingStrategy
    {
        static ServerIndexNamingStrategy()
        {
            PropertyNameNeedPrefix = new[]
            {
                nameof(ServerIndexContext.Vocab),
                nameof(ServerIndexModel.Context),
                nameof(ServerIndexResourceModel.Id),
                nameof(ServerIndexResourceModel.Type),
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
