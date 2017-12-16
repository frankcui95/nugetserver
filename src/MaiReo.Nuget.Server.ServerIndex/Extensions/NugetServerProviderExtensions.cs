using MaiReo.Nuget.Server.Configurations.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Threading.Tasks;
using MaiReo.Nuget.Server.Models;
using MaiReo.Nuget.Server.Serialization;
using System.IO;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Serialization;

namespace MaiReo.Nuget.Server.Core
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class NugetServerProviderExtensions
    {
        public static async Task RespondServerIndexAsync(
            this INugetServerProvider provider,
            HttpContext context)
        {
            var serializer = provider.CreateJsonSerializerForServiceIndex();
            var baseUrl = context.GetBaseUrl();
            var versionPrefix = "/v" + provider.NugetServerOptions.GetApiMajorVersion();
            var serverIndex = new ServerIndex(baseUrl + versionPrefix, provider.NugetServerOptions)
            {
                Version = provider.NugetServerOptions.ApiVersion,
                Context = ServerIndexContext.Default
            };

            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, serverIndex);
            }

            context.Response.StatusCode
                = (int)System.Net.HttpStatusCode.OK;
            using (var content = new StringContent(sb.ToString(),
                Encoding.UTF8,
                "application/json"))
            {
                if (HttpMethods
                    .IsHead(context.Request.Method))
                {
                    context.Response.ContentLength
                        = content.Headers.ContentLength;
                }
                else if (HttpMethods
                    .IsGet(context.Request.Method))
                {
                    await Task.Run(
                        () =>
                        content
                        .CopyToAsync(context.Response.Body),
                            context.RequestAborted);
                }
            }


        }

        public static JsonSerializer CreateJsonSerializerForServiceIndex(
            this INugetServerProvider provider)
        {
            var serializer = provider.CreateJsonSerializer();
            serializer.ContractResolver = new ServerIndexContractResolver();
            return serializer;
        }
    }
}
