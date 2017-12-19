using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Nuget.Server.Events
{
    public delegate void
    AddPackageCompletedEventHandler(
        object sender,
        AddPackageCompletedEventArgs args );

    public delegate void 
    AddPackageFailedEventHandler(
        object sender,
        AddPackageFailedEventArgs args );
    
}
