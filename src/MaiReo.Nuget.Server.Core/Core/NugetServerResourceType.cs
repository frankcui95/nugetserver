using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MaiReo.Nuget.Server
{
    public enum NugetServerResourceType
    {
        PackageBaseAddress,
        PackagePublish,
        RegistrationsBaseUrl,
        RegistrationsBaseUrl_3_4_0,
        SearchQueryService,
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class NugetServerResourceTypes
    {
        public const string PackageBaseAddress = nameof(PackageBaseAddress);
        public const string PackagePublish = nameof(PackagePublish);
        public const string RegistrationsBaseUrl = nameof(RegistrationsBaseUrl);
        public const string RegistrationsBaseUrl_3_4_0 = nameof(RegistrationsBaseUrl) + "/3.4.0";
        public const string SearchQueryService = nameof(SearchQueryService);

        public static string GetText(this NugetServerResourceType resourceType)
        {
            switch (resourceType)
            {
                case NugetServerResourceType.PackageBaseAddress:
                    return PackageBaseAddress;
                case NugetServerResourceType.PackagePublish:
                    return PackagePublish;
                case NugetServerResourceType.RegistrationsBaseUrl:
                    return RegistrationsBaseUrl;
                case NugetServerResourceType.RegistrationsBaseUrl_3_4_0:
                    return RegistrationsBaseUrl_3_4_0;
                case NugetServerResourceType.SearchQueryService:
                    return SearchQueryService;
                default:
                    break;
            }
            throw new NotSupportedException();
        }
    }

}
