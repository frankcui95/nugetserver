using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MaiReo.Nuget.Server.Models
{
    [Serializable]
    [XmlRoot( "package" )]
    public class Nuspec
    {
        [XmlElement( "metadata" )]
        public NuspecMetadata Metadata { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public string FilePath { get; set; }

        static Nuspec()
        {
            SerializerFactory = new XmlSerializerFactory();
            NuspecSerializer = SerializerFactory.CreateSerializer( typeof( Nuspec ) );
        }
        private static readonly XmlSerializerFactory SerializerFactory;

        private static readonly XmlSerializer NuspecSerializer;



        public static bool TryParse( byte[] metadata, out Nuspec nuspec )
        {
            nuspec = null;
            if (metadata == null) return false;
            using (var inputStream = new MemoryStream( metadata ))
                return TryParse( inputStream, out nuspec );
        }

        public static bool TryParse( Stream stream, out Nuspec nuspec )
        {
            nuspec = null;
            if (stream == null) return false;
            try
            {
                using (var xmlReader = new XmlTextReader( stream ))
                {
                    //annoying diff xmlns
                    xmlReader.Namespaces = false;
                    nuspec = (Nuspec)NuspecSerializer.Deserialize( xmlReader );
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }
    }

}
