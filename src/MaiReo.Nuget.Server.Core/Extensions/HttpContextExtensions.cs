using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MaiReo.Nuget.Server.Core;

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
            url.HasValue

            ? context.Request.Path == url
            : context.IsRequestingRootUrl();

        public static bool IsRequestingRootUrl(
            this HttpContext context)
            =>
            context
            .Request
            .Path == "/";
    }
}
