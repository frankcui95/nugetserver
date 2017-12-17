using System;
using System.Xml.Serialization;

namespace MaiReo.Nuget.Server.Models
{
    [Serializable]
    public class NuspecMetadataRepository
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }
    }
}
