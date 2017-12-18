using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace MaiReo.Nuget.Server.Tools
{
    public static class Gzip
    {
        public static byte[] Write(StringBuilder stringBuilder,
            Encoding encoding,
            CompressionLevel level = CompressionLevel.Optimal)
        {
            var bytes = encoding.GetBytes(stringBuilder.ToString());
            var baseStream = new MemoryStream();
            using (var gzipStream = new GZipStream(baseStream, level, true))
            {
                gzipStream.Write(bytes, 0, bytes.Length);
            }
            return baseStream.ToArray();
        }
    }
}
