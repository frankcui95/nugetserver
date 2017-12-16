﻿using MaiReo.Nuget.Server.Configurations;
using MaiReo.Nuget.Server.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.Middlewares
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class NugetServerSearchQueryServiceMiddleware : IMiddleware
    {
        private readonly INugetServerProvider _nugetServerProvider;

        public NugetServerSearchQueryServiceMiddleware(
            INugetServerProvider nugetServerProvider)
        {
            this._nugetServerProvider = nugetServerProvider;
        }
        public async Task InvokeAsync(
            HttpContext context,
            RequestDelegate next)
        {
            if (context
                .IsRequestingSearchQueryService(
                _nugetServerProvider.NugetServerOptions))
            {
                await _nugetServerProvider
                      .RespondSearchQueryServiceAsync(context);
                return;
            }

            await next(context);
            return;
        }
    }
}