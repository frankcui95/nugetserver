using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MaiReo.Nuget.Server.Core;
using MaiReo.Nuget.Server.Core.Extensions;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpContextExtensions
    {

        public static bool IsRequestingServerIndex(
            this HttpContext context,
            NugetServerOptions options)
        {
            if (!HttpMethods
                .IsHead(context.Request.Method)
                && !HttpMethods
                .IsGet(context.Request.Method))
            {
                return false;
            }
            var url = options
                .GetServiceIndexUrlPath();

            return context
                .IsRequestingUrl(url);
        }

    }
}
