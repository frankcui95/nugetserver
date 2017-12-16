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

        public static string GetBaseUrl(
            this HttpContext context)
            =>
            context.Request.Protocol +
            context.Request.Host +
            "/" +
            context.Request.PathBase;
       
        public static bool IsRequestingUrl(
            this HttpContext context,
            string url)
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
