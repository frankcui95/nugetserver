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
        RegistrationsBaseUrl_3_0_0_beta,
        RegistrationsBaseUrl_3_0_0_rc,
        RegistrationsBaseUrl_3_4_0,
        RegistrationsBaseUrl_3_6_0,
        SearchQueryService,
        SearchQueryService_3_0_0_beta,
        SearchQueryService_3_0_0_rc
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class NugetServerResourceTypes
    {
        public const string PackageBaseAddress = nameof(PackageBaseAddress) + "/3.0.0";
        public const string PackagePublish = nameof(PackagePublish);
        public const string RegistrationsBaseUrl = nameof(RegistrationsBaseUrl);
        public const string RegistrationsBaseUrl_3_0_0_beta = nameof(RegistrationsBaseUrl) + "/3.0.0-beta";
        public const string RegistrationsBaseUrl_3_0_0_rc = nameof(RegistrationsBaseUrl) + "/3.0.0-rc";
        public const string RegistrationsBaseUrl_3_4_0 = nameof(RegistrationsBaseUrl) + "/3.4.0";
        public const string RegistrationsBaseUrl_3_6_0 = nameof(RegistrationsBaseUrl) + "/3.6.0";
        public const string SearchQueryService = nameof(SearchQueryService);
        public const string SearchQueryService_3_0_0_beta = nameof(SearchQueryService) + "/3.0.0-beta";
        public const string SearchQueryService_3_0_0_rc = nameof(SearchQueryService) + "/3.0.0-rc";

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
                case NugetServerResourceType.RegistrationsBaseUrl_3_0_0_beta:
                    return RegistrationsBaseUrl_3_0_0_beta;
                case NugetServerResourceType.RegistrationsBaseUrl_3_0_0_rc:
                    return RegistrationsBaseUrl_3_0_0_rc;
                case NugetServerResourceType.RegistrationsBaseUrl_3_4_0:
                    return RegistrationsBaseUrl_3_4_0;
                case NugetServerResourceType.RegistrationsBaseUrl_3_6_0:
                    return RegistrationsBaseUrl_3_6_0;
                case NugetServerResourceType.SearchQueryService:
                    return SearchQueryService;
                case NugetServerResourceType.SearchQueryService_3_0_0_beta:
                    return SearchQueryService_3_0_0_beta;
                case NugetServerResourceType.SearchQueryService_3_0_0_rc:
                    return SearchQueryService_3_0_0_rc;
                default:
                    break;
            }
            throw new NotSupportedException();
        }
    }

}
