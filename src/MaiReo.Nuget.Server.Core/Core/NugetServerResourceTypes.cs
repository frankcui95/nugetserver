using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Nuget.Server
{
    public sealed class NugetServerResourceTypes
    {
        public const string PackageBaseAddress = nameof(PackageBaseAddress);
        public const string PackagePublish = nameof(PackagePublish);
        public const string RegistrationsBaseUrl = nameof(RegistrationsBaseUrl);
        public const string RegistrationsBaseUrl_3_4_0 = nameof(RegistrationsBaseUrl) + "/3.4.0" ;
        public const string SearchQueryService = nameof(SearchQueryService);
    }
}
