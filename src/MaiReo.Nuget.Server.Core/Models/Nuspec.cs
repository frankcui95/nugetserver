using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MaiReo.Nuget.Server.Models
{
    [Serializable]
    [XmlRoot("package")]
    public class Nuspec
    {
        [XmlElement("metadata")]
        public NuspecMetadata Metadata { get; set; }
    }

}
