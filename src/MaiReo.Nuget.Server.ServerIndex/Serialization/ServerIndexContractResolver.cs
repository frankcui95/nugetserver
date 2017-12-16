using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MaiReo.Nuget.Server.Serialization
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ServerIndexContractResolver : DefaultContractResolver
    {
        public ServerIndexContractResolver()
        {
            NamingStrategy = new ServerIndexNamingStrategy();
        }
    }
}
