namespace Flogging.Core
{
    public interface IContainUserLogInfo
    {
        string UserId { get; set; }
        string UserName { get; set; }
        int CustomerId { get; set; }
        string CustomerName { get; set; }
    }
}