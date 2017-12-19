using MaiReo.Nuget.Server.Events;
using Microsoft.AspNetCore.Http;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MaiReo.Nuget.Server.Core
{
    public class NugetServerOptions
    {
        [EditorBrowsable( EditorBrowsableState.Never )]
        public const string DEFAULT_API_VERSION = "3.0.0-beta.1";

        [EditorBrowsable( EditorBrowsableState.Never )]
        public const string DEFAULT_SERVICE_INDEX_V3 = "/index.json";

        [EditorBrowsable( EditorBrowsableState.Never )]
        public const string DEFAULT_Package_Directory = "packages";

        public const string DEFAULT_API_KEY = "I_DONT_THINK_IT_IS_A_SECRET_KEY";

        [EditorBrowsable( EditorBrowsableState.Never )]
        public NugetServerOptions()
        {
            ApiKey = DEFAULT_API_KEY;
            ApiVersion = NuGetVersion.Parse( DEFAULT_API_VERSION );
            ServiceIndex = DEFAULT_SERVICE_INDEX_V3;
            PackageDirectory = DEFAULT_Package_Directory;
            Resources = new Dictionary<NugetServerResourceType, PathString>();
            InterpretUnListEnabled = false;
        }

        /// <summary>
        /// A string For Push and Delete.
        /// Default is <see cref="DEFAULT_API_KEY"/>
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or set nuget server api version. Only supports 3.0.0-beta.1 currently.
        /// </summary>
        public NuGetVersion ApiVersion { get; set; }

        /// <summary>
        /// For more infomation, please visit https://docs.microsoft.com/en-us/nuget/api/service-index .
        /// </summary>
        public PathString ServiceIndex { get; set; }
        /// <summary>
        /// Gets or set nuget package (.nupkg) directory root path.
        /// Default is "packages".
        /// Case-(in)sensitive depends on platform.
        /// </summary>
        public string PackageDirectory { get; set; }

        /// <summary>
        /// For more infomation, please visit https://docs.microsoft.com/en-us/nuget/api/service-index .
        /// </summary>
        public IDictionary<NugetServerResourceType, PathString> Resources { get; private set; }

        /// <summary>
        /// Interprets the package delete request as an "unlist".
        /// For more infomation, please visit https://docs.microsoft.com/en-us/nuget/api/package-publish-resource
        /// </summary>
        public bool InterpretUnListEnabled { get; set; }

        /// <summary>
        /// Fires when a package is successfully added.
        /// </summary>
        public event AddPackageCompletedEventHandler AddPackageCompleted;

        /// <summary>
        /// Fires when a package is failed to add.
        /// </summary>
        public event AddPackageFailedEventHandler AddPackageFailed;

        /// <summary>
        /// Fires when a package is successfully deleted.
        /// </summary>
        public event DeletePackageCompletedEventHandler DeletePackageCompleted;

        /// <summary>
        /// Fires when a package is failed to delete.
        /// </summary>
        public event DeletePackageFailedEventHandler DeletePackageFailed;

        internal async Task<bool> InvokeAddPackageCompletedAsync(
            object sender,
            AddPackageCompletedEventArgs eventArgs )
        {
            if (AddPackageCompleted == null) return false;
            try
            {
                await Task.Factory.FromAsync(
                        AddPackageCompleted.BeginInvoke,
                        AddPackageCompleted.EndInvoke
                        , sender, eventArgs, state: default( object )
                        );
            }
            catch
            {
            }
            return true;
        }

        internal async Task<bool> InvokeAddPackageFailedAsync(
            object sender,
            AddPackageFailedEventArgs eventArgs )
        {
            if (AddPackageFailed == null) return false;
            try
            {
                await Task.Factory.FromAsync(
                        AddPackageFailed.BeginInvoke,
                        AddPackageFailed.EndInvoke
                        , sender, eventArgs, state: default( object )
                        );
            }
            catch
            {
            }

            if (eventArgs is IHandleableEventArgs handleable
                && handleable != null
                && (!handleable.IsHandled)
                && handleable.Exception != null)
            {
                throw handleable.Exception;
            }
            return true;
        }
        internal async Task<bool> InvokeDeletePackageCompletedAsync(
            object sender,
            DeletePackageCompletedEventArgs eventArgs )
        {
            if (DeletePackageCompleted == null) return false;
            try
            {
                await Task.Factory.FromAsync(
                DeletePackageCompleted.BeginInvoke,
                DeletePackageCompleted.EndInvoke
                , sender, eventArgs, state: default( object )
                );
            }
            catch
            {
            }
            return true;
        }

        internal async Task<bool> InvokeDeletePackageFailedAsync(
            object sender,
            DeletePackageFailedEventArgs eventArgs )
        {
            if (DeletePackageFailed == null) return false;
            try
            {
                await Task.Factory.FromAsync(
                DeletePackageFailed.BeginInvoke,
                DeletePackageFailed.EndInvoke
                , sender, eventArgs, state: default( object )
                );
            }
            catch
            {
            }

            if (eventArgs is IHandleableEventArgs handleable
                && handleable != null
                && (!handleable.IsHandled)
                && handleable.Exception != null)
            {
                throw handleable.Exception;
            }
            return true;
        }
    }
}