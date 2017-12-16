using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MaiReo.Nuget.Server.Configurations;
using MaiReo.Nuget.Server.Configurations.Extensions;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpContextExtensions
    {

        /// <summary>
        /// How to get the correct base url
        /// when app is NOT hosting at the root path "/" ?
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetBaseUrl(
            this HttpContext context)
            =>
            context.Request.Scheme +
            "://" +
            context.Request.Host;

        public static bool IsRequestingUrl(
            this HttpContext context,
            PathString url)
            =>
            string.IsNullOrWhiteSpace(url)
            ? context.IsRequestingRootUrl()
            : context.Request.Path == url;

        public static bool IsRequestingRootUrl(
            this HttpContext context)
            =>
            context
            .Request
            .Path == "/";
    }
}
