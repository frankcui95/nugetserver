using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MaiReo.Nuget.Server.Models
{
    [Serializable]
    public class NuspecMetadataDependencyGroup
    {
        [XmlAttribute("targetFramework")]
        public string TargetFramework { get; set; }

        [XmlElement("dependency")]
        public List<NuspecMetadataDependency> Dependency { get; set; }
    }
}
