using System.Collections.Generic;
using System.ComponentModel;

namespace MaiReo.Nuget.Server.Models
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ServerIndexResourceModelComparer : IEqualityComparer<ServerIndexResourceModel>
    {
        static ServerIndexResourceModelComparer()
            =>
            Instance = new ServerIndexResourceModelComparer();

        public static ServerIndexResourceModelComparer Instance { get; private set; }

        public bool Equals(
            ServerIndexResourceModel x,
            ServerIndexResourceModel y)
            =>
            object.ReferenceEquals(x, y)
            || string.Equals(x?.Type, y?.Type,
                System
                .StringComparison
                .CurrentCultureIgnoreCase);

        public int GetHashCode(
            ServerIndexResourceModel obj)
            => obj
            ?.Type
            ?.ToLower()
            ?.GetHashCode()
            ?? 0;
    }
}