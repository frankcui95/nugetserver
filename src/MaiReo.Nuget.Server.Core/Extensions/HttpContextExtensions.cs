using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MaiReo.Nuget.Server.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq;
using System.ComponentModel;
using System.Collections.Concurrent;
using Microsoft.Extensions.Primitives;
using System.Collections;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpContextExtensions
    {
        public static T FromQueryString<T>(
            this HttpContext context)
            where T : class, new()
            =>
            context.Request.Query.As<T>();

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

        public static bool IsRequestingUrls(
            this HttpContext context,
            IEnumerable<PathString> urls)
            =>
            urls?.Any() == true
            ? context.Request.Path.In(urls, (x, y) => x.StartsWithSegments(y))
            : context.IsRequestingRootUrl();

        public static bool IsRequestingUrl(
            this HttpContext context,
            PathString url)
            =>
            url.HasValue

            ? context.Request.Path.StartsWithSegments(url)
            : context.IsRequestingRootUrl();

        public static bool IsRequestingRootUrl(
            this HttpContext context)
            =>
            context
            .Request
            .Path == "/";
    }
}
