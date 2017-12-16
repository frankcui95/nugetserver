using System.Collections.Generic;
using System.ComponentModel;

namespace MaiReo.Nuget.Server.Models
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ServerIndexResourceComparer : IEqualityComparer<ServerIndexResource>
    {
        static ServerIndexResourceComparer()
            =>
            Instance = new ServerIndexResourceComparer();

        public static ServerIndexResourceComparer Instance { get; private set; }

        public bool Equals(
            ServerIndexResource x,
            ServerIndexResource y)
            =>
            object.ReferenceEquals(x, y)
            || string.Equals(x?.Type, y?.Type,
                System
                .StringComparison
                .CurrentCultureIgnoreCase);

        public int GetHashCode(
            ServerIndexResource obj)
            => obj
            ?.Type
            ?.ToLower()
            ?.GetHashCode()
            ?? 0;
    }
}