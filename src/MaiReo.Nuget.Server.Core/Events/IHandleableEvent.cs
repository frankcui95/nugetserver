namespace MaiReo.Nuget.Server.Events
{
    public interface IHandleableEventArgs
    {
        System.Exception Exception { get; }
        bool IsHandled { get; set; }
    }
}