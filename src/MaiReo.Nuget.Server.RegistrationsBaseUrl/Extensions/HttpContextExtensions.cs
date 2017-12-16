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
        public static bool IsRequestingRegistrationsBaseUrl(
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
                .GetRegistrationsBaseUrlUrlPath();

            return context
                .IsRequestingUrl(url);
        }
    }
}
