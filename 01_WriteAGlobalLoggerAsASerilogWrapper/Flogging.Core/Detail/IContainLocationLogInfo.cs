namespace Flogging.Core
{
    public interface IContainLocationLogInfo
    {
        string Product { get; set; }
        string Layer { get; set; }
        string Location { get; set; }
        string Hostname { get; set; }
    }
}
