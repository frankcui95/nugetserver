using MaiReo.Nuget.Server.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.Core
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class NugetServerPackageBaseAddressMiddleware : IMiddleware
    {
        private readonly INugetServerProvider _nugetServerProvider;

        public NugetServerPackageBaseAddressMiddleware(
            INugetServerProvider nugetServerProvider)
        {
            this._nugetServerProvider = nugetServerProvider;
        }
        public async Task InvokeAsync(
            HttpContext context,
            RequestDelegate next)
        {
            if (context
                .IsRequestingPackageBaseAddress(
                _nugetServerProvider.NugetServerOptions))
            {
                await _nugetServerProvider
                      .RespondPackageBaseAddressAsync(context);
                return;
            }

            await next(context);
            return;
        }
    }
}
