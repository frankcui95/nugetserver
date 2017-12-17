using MaiReo.Nuget.Server.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MaiReo.Nuget.Server.Models
{
    [Serializable]
    public class NuspecMetadata
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("version")]
        public string Version { get; set; }

        [XmlElement("authors")]
        public string Authors { get; set; }

        [XmlElement("owners")]
        public string Owners { get; set; }

        [XmlElement("requireLicenseAcceptance")]
        public bool RequireLicenseAcceptance { get; set; }

        [XmlElement("licenseUrl")]
        public string LicenseUrl { get; set; }

        [XmlElement("projectUrl")]
        public string ProjectUrl { get; set; }

        [XmlElement("iconUrl")]
        public string IconUrl { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("releaseNotes")]
        public string ReleaseNotes { get; set; }

        [XmlElement("tags")]
        public string Tags { get; set; }

        [XmlElement("copyright")]
        public string Copyright { get; set; }

        [XmlElement("repository")]
        public NuspecMetadataRepository Repository { get; set; }

        [XmlArray("dependencies")]
        [XmlArrayItem("group")]
        public List<NuspecMetadataDependencyGroup> Dependencies { get; set; }

    }
}
