using MaiReo.Nuget.Server.Core;
using MaiReo.Nuget.Server.RegistrationsBaseUrl;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.Middlewares
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class RegistrationsBaseUrlMiddleware : IMiddleware
    {
        private readonly INugetServerProvider _nugetServerProvider;

        public RegistrationsBaseUrlMiddleware(
            INugetServerProvider nugetServerProvider)
        {
            this._nugetServerProvider = nugetServerProvider;
        }
        public async Task InvokeAsync(
            HttpContext context,
            RequestDelegate next)
        {
            if (_nugetServerProvider.IsMatch(context))
            {
                await _nugetServerProvider
                      .RespondAsync(context);
                return;
            }

            await next(context);
            return;
        }
    }
}
