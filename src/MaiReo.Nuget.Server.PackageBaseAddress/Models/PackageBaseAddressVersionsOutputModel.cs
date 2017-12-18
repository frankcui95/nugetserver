using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Nuget.Server.Models
{
    public class PackageBaseAddressVersionsOutputModel
    {
        public PackageBaseAddressVersionsOutputModel()
        {
            Versions = new List<string>(0);
        }
        public List<string> Versions { get; set; }
    }
}
