using System.Collections.Generic;

namespace Flogging.Core
{
    public interface IContainAddionalLogInfo
    {
        Dictionary<string, object> AdditionalInfo { get; set; }
    }
}
