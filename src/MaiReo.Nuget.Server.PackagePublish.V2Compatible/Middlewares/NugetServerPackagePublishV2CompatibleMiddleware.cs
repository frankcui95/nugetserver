using MaiReo.Nuget.Server.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MaiReo.Nuget.Server.Extensions;

namespace MaiReo.Nuget.Server.Middlewares
{
    public class NugetServerPackagePublishV2CompatibleMiddleware 
    : IMiddleware
    {
        private readonly INugetServerProvider
            _nugetServerProvider;

        public NugetServerPackagePublishV2CompatibleMiddleware(
            INugetServerProvider nugetServerProvider )
        {
            this._nugetServerProvider = nugetServerProvider;
        }
        public async Task InvokeAsync(
            HttpContext context,
            RequestDelegate next )
        {

            if (_nugetServerProvider
                .IsMatchPackagePublishV2Compatible( context ))
            {
                await _nugetServerProvider
                  .RespondPackagePublishV2CompatibleAsync( context );
                return;
            }

            await next( context );
            return;
        }
    }
}
