using MaiReo.Nuget.Server.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Nuget.Server.SearchQueryService.Models
{
    public class SearchModel
    {
        /// <summary>
        /// The search terms to used to filter packages
        /// </summary>
        public string Q { get; set; }
        /// <summary>
        /// The number of results to skip, for pagination
        /// </summary>
        public int Skip { get; set; }
        /// <summary>
        /// The number of results to return, for pagination
        /// </summary>
        public int Take { get; set; }
        /// <summary>
        /// true or false determining whether to include pre-release packages
        /// </summary>
        public bool PreRelease { get; set; }
        /// <summary>
        /// A SemVer 1.0.0 version string
        /// </summary>
        public VersionString SemVerLevel { get; set; }
    }
}
