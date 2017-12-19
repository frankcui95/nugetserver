using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Nuget.Server.Events
{
    
    public delegate void 
    DeletePackageCompletedEventHandler( 
        object sender,
        DeletePackageCompletedEventArgs args );

    public delegate void
    DeletePackageFailedEventHandler(
        object sender,
        DeletePackageFailedEventArgs args );

}
