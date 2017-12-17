using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;
using MaiReo.Nuget.Server.Models;
using System.Xml;

namespace MaiReo.Nuget.Server.Tools
{
    public static class Zip
    {
        static Zip()
        {
            SerializerFactory = new XmlSerializerFactory();
            NuspecSerializer = SerializerFactory.CreateSerializer(typeof(Nuspec));
        }
        private static readonly XmlSerializerFactory SerializerFactory;

        private static readonly XmlSerializer NuspecSerializer;

        public static Nuspec ReadNuspec(string fileName)
        {
            try
            {
                using (var arc = ZipFile.OpenRead(fileName))
                {
                    var nuspec = arc.Entries
                        .Where(e => e.FullName == e.Name)
                        .FirstOrDefault(e => e.Name.EndsWith(".nuspec"));
                    if (nuspec == null) return default(Nuspec);
                    using (var zipStream = nuspec.Open())
                    using (var xmlReader = new XmlTextReader(zipStream))
                    {
                        //annoying diff xmlns
                        xmlReader.Namespaces = false;
                        return (Nuspec)NuspecSerializer.Deserialize(xmlReader);
                    }
                }
            }
            catch
            {
            }
            return default(Nuspec);
        }
    }
}
